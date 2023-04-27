using KindHands.Data;
using KindHands.Models;
using KindHands.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace KindHands.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {

        private readonly KindHandsContext _context;
        private readonly PasswordHasher _passwordHasher;

        public LoginController(KindHandsContext context, PasswordHasher passwordHasher)
        {
            _context = context;
            this._passwordHasher = passwordHasher;
        }

        [HttpGet]
        [Route("/login")]
        public IActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        [Route("/login")]
        public async Task<IActionResult> LoginUser(LoginViewModel model, string returnUrl = "")
        {
            var loginValid = await ValidateLogin(model.Username, model.Password);

            if (!loginValid.Success)
            {
                TempData["LoginFailed"] = $"Логин / пароль не корректны.";

                return Redirect("/login");
            }
            else
            {
                await SignInUser(loginValid.User);

                if (string.IsNullOrWhiteSpace(returnUrl) || !returnUrl.StartsWith("/"))
                {
                    returnUrl = "/";
                }

                return this.Redirect(returnUrl);
            }
        }

        [HttpPost]
        [Route("/logout")]
        [Authorize]
        public async Task<IActionResult> Logout(LoginViewModel model, string returnUrl = "")
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Redirect("~/");
        }

        [NonAction]
        private async Task<ValidateLoginResult> ValidateLogin(string username, string password)
        {
            //1 Получим пользователя по его ID
            var users = from u in _context.Users.AsNoTracking()
                        where u.UserName == username
                        select u;

            var user = await users.SingleOrDefaultAsync();

            if (user != null && _passwordHasher.IsPasswordSame(password, user))
            {
                return new ValidateLoginResult { Success = user != null, User = user };
            }

            return new ValidateLoginResult { Success = false };

        }

        private async Task SignInUser(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };

            if (user.IsAdmin)
            {
                claims.Add(new Claim(ClaimTypes.Role, "ROLE_ADMIN"));
            }

            if (user.IsModerator)
            {
                claims.Add(new Claim(ClaimTypes.Role, "ROLE_MODERATOR"));
            }

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));
        }
    }

    class ValidateLoginResult
    {
        public bool Success { get; set; }

        public User? User { get; set; }
    }
}

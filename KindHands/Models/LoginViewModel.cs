using System.ComponentModel.DataAnnotations;

namespace KindHands.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Введите имя пользователя")]
        [Display(Name = nameof(Username), ShortName = nameof(Username), ResourceType = typeof(LoginViewModelStrings))]
        public string Username { get; set; } = string.Empty;


        [Required(ErrorMessage = "Введите пароль пользователя")]
        [DataType(DataType.Password)]
        [Display(Name = nameof(Password), ShortName = nameof(Password), ResourceType = typeof(LoginViewModelStrings))]
        public string Password { get; set; } = string.Empty;


        [Display(Name = nameof(RememberMe), ShortName = nameof(RememberMe), ResourceType = typeof(LoginViewModelStrings))]
        public bool RememberMe { get; set; }
    }
}

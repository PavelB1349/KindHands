using System.Security.Claims;

namespace KindHands.Services
{
    public class DefaultUserProvider
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public DefaultUserProvider(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public int? CurrentUserId()
        {
            // взял в try, чтобы продолжала работать.
            try
            {
                var currentUser = httpContextAccessor.HttpContext?.User;

                if (currentUser == null)
                {
                    return null;
                }
                // вот здесь выходит исключение "Sequence contains no matching element", но пользователь добавляется в БД
                var claim = currentUser.Claims
                    .First((x) => x.Type == ClaimTypes.NameIdentifier);

                if (claim == null)
                {
                    return null;
                }

                if (int.TryParse(claim.Value, out var userId))
                {
                    return userId;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }


            return null;
        }
    }
}
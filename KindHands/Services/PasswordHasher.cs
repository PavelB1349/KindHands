using KindHands.Models;
using System.Text;

namespace KindHands.Services
{
    public class PasswordHasher
    {
        public string CreateHash(string password)
        {
            // 1 получим байты строки
            var bytes = Encoding.UTF8.GetBytes(password);

            //2 Полуим "хэш"
            return Convert.ToBase64String(bytes);
        }

        public bool IsPasswordSame(string password, User user)
        {
            var storedHash = user.Password;
            var currentPasswHash = CreateHash(password);

            return string.Compare(storedHash
                , currentPasswHash
                , StringComparison.OrdinalIgnoreCase) == 0;
        }
    }
}
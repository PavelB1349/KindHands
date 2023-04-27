using System.Security.Cryptography;
using System.Text;

namespace KindHands.Helpers
{
    public static class HashPasswordHelper
    {
        public static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create() )
            {
                var hashedBytes = sha256.ComputeHash(buffer:Encoding.UTF8.GetBytes(password));
                var hash = BitConverter.ToString(hashedBytes).Replace(oldValue:"-", newValue:"").ToLower();
                return hash;
            }
        }
    }
}

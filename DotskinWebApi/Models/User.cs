using System.Text;
using System.Text.Json.Serialization;
using System.Security.Cryptography;

namespace DotskinWebApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
       

        public List<Order> Orders { get; set; }



        public User(string userName, string password, string email)
        {
            UserName = userName;
            Email = email;
            PasswordHash = HashPassword(password);

        }
        public User() { }

        public string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
    }
}

using System.Text.Json.Serialization;

namespace DotskinWebApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
       

        public List<Order> Orders { get; set; }



        public User(int id, string userName, string password, string email)
        {
            Id = id;
            UserName = userName;
            Email = email;
            PasswordHash = password;

        }
        public User() { }
    }
}

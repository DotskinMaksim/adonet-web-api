using System.Text.Json.Serialization;

namespace DotskinWebApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<Order> Orders { get; set; }



        public User(int id, string userName, string password, string firstName, string lastName)
        {
            Id = id;
            UserName = userName;
            PasswordHash = password;
            FirstName = firstName;
            LastName = lastName;        }

        public User() { }
    }
}

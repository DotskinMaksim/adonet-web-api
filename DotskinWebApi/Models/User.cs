using System.Text.Json.Serialization;

namespace DotskinWebApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [JsonIgnore]

        public ICollection<Product> Products { get; set; }

        public User(int id, string userName, string password, string firstName, string lastName)
        {
            Id = id;
            UserName = userName;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            Products = new List<Product>(); 
        }
    }
}

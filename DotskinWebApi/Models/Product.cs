using System.Text.Json.Serialization;

namespace DotskinWebApi.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public bool IsActive { get; set; }

        // Добавляем коллекцию пользователей
        [JsonIgnore]
        public ICollection<User> Users { get; set; }

        public Product(int id, string name, double price, bool isActive)
        {
            Id = id;
            Name = name;
            Price = price;
            IsActive = isActive;
            Users = new List<User>();  // Инициализируем пустую коллекцию пользователей
        }
    }
}

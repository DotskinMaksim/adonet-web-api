using System.Text.Json.Serialization;

namespace DotskinWebApi.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public bool IsActive { get; set; }

        public List<OrderItem> OrderItems { get; set; }


        public Product(int id, string name, double price, bool isActive)
        {
            Id = id;
            Name = name;
            Price = price;
            IsActive = isActive;
     
        }
    }
}

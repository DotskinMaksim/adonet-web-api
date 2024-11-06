using System.Text.Json.Serialization;

namespace DotskinWebApi.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public double PricePerUnit { get; set; }

        public int AmountInStock { get; set; }

        public string Unit { get; set; }  

        public string Category { get; set; } 

        public bool HasBottle { get; set; }

        public bool IsActive { get; set; }
        public string ImageUrl { get; set; }

        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        public Product(int id, string name, double pricePerUnit, bool isActive, string unit, bool hasBottle, string imageUrl, int amountInStock, string category)
        {
            Id = id;
            Name = name;
            PricePerUnit = pricePerUnit;
            IsActive = isActive;
            Unit = unit;
            HasBottle = hasBottle;
            ImageUrl = imageUrl;
            AmountInStock = amountInStock;
            Category = category;
        }
    }
}

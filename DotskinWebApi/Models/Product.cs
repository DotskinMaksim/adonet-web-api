using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotskinWebApi.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public string NameEt { get; set; }
        public string NameEn { get; set; }
        public string NameRu { get; set; }
        
        public double PricePerUnit { get; set; }

        public double AmountInStock { get; set; }

        public string Unit { get; set; }  

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; } 
        public bool HasBottle { get; set; }

        public bool IsActive { get; set; }
        public string ImageUrl { get; set; }

        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        public Product(string name, double pricePerUnit, bool isActive, string unit, bool hasBottle, string imageUrl, double amountInStock, int categoryId)
        {
            Name = name;
            PricePerUnit = pricePerUnit;
            IsActive = isActive;
            Unit = unit;
            HasBottle = hasBottle;
            ImageUrl = imageUrl;
            AmountInStock = amountInStock;
            CategoryId = categoryId;
        }
        public string GetName(string language)
        {
            return language switch
            {
                "ru" => NameRu,
                "en" => NameEn,
                _ => NameEt
            };
        }
    }
}

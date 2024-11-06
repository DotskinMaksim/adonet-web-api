using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DotskinWebApi.Models
{
    public class OrderItem
    {
        public int Id { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public double Quantity { get; set; } 

        public double TotalPrice => Product.PricePerUnit * Quantity;

        [JsonIgnore]
        public Order Order { get; set; }

        [JsonIgnore]
        public Product Product { get; set; }
    }
}

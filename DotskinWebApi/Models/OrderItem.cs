using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DotskinWebApi.Models
{
    public class OrderItem
    {
        public int Id { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }

        [JsonIgnore]
        public Order Order { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        [JsonIgnore]
        public Product Product { get; set; }

        public int Quantity { get; set; }
    }
}

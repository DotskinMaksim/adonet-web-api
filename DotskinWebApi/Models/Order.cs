using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DotskinWebApi.Models
{
    public class Order
    {
        public int Id { get; set; }

        public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        public bool IsPaid { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [JsonIgnore]
        public User User { get; set; }

        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    }
}

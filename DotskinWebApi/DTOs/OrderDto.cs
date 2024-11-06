namespace DotskinWebApi.DTOs
{
    public class CreateOrderItemDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public string Unit { get; set; }       
        public double PricePerUnit { get; set; } 
    }

    public class CreateOrderDto
    {
        public bool IsPaid { get; set; }
        public int UserId { get; set; }
        public List<CreateOrderItemDto> OrderItems { get; set; }
    }
}

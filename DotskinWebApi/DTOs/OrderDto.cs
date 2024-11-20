namespace DotskinWebApi.DTOs
{
    public class CreateOrderItemDto
    {
        public int ProductId { get; set; }
        public double Quantity { get; set; }
        
    }

    public class CreateOrderDto
    {
        public List<CreateOrderItemDto> OrderItems { get; set; }
    }
}

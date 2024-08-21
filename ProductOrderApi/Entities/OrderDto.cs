namespace ProductOrderApi.Entities
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string? CustomerName { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderDetailDto>? OrderDetails { get; set; }
    }
}


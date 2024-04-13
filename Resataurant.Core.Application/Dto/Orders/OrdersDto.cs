namespace Restaurant.Core.Application.Dto.Orders
{
    public class OrdersDto
    {
        public int Id { get; set; }
        public decimal Subtotal { get; set; }
        public List<int>? PlateOrders { get; set; }
        public List<string>? Plates {  get; set; } 
        public int TableId { get; set; }
        public  string? Table { get; set; }
        public string OrderStatus { get; set; }
        public int OrderStatusId { get; set; }
    }
}

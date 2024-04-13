namespace Restaurant.Core.Application.Dto.Orders
{
    public class OrderAddDto
    {
        public int Id { get; set; }
        public float Subtotal { get; set; }
        public List<int> PlateOrders { get; set; }
        public int TableId { get; set; }
    }
}

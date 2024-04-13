namespace Restaurant.Core.Application.Dto.Orders
{
    public class OrderPlatesUpdate
    {
        public int Id { get; set; }
        public List<int> PlateList { get; set; }
    }
}

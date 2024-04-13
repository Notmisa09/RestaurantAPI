using Restaurant.Core.Domain.Core;

namespace Restaurant.Core.Domain.Entities
{
    public class Orders : BaseEntity
    {
        public decimal Subtotal { get; set; }
        
        //Navegation Properties
        public ICollection<PlateOrders>? PlateOrders { get; set; }
        public int TableId { get; set; }
        public Tables? Table { get; set; }  
        public int OrderStatusId { get; set; }
        public OrderStatus? OrderStatus { get; set; }

    }
}

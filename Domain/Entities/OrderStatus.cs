using Restaurant.Core.Domain.Core;

namespace Restaurant.Core.Domain.Entities
{
    public class OrderStatus : BaseEntity
    {
       public string Description { get; set; }
       public ICollection<Orders> Orders { get; set; }
    }
}

using Restaurant.Core.Domain.Core;

namespace Restaurant.Core.Domain.Entities
{
    public class Ingridients : BaseEntity
    {
        public string Nombre { get; set; }
        public ICollection<PlatesIngridients> PlatesIngridients { get; set; }
    }
}

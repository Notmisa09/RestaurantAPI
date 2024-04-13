using Restaurant.Core.Domain.Core;

namespace Restaurant.Core.Domain.Entities
{
    public class PlatesCategories : BaseEntity
    {
        public string Description { get; set; }

        //Navegation properties
        public ICollection<Plates>? Plates { get; set; }
    }
}

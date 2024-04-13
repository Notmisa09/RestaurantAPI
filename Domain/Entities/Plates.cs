using Restaurant.Core.Domain.Core;
using Restaurant.Core.Domain.Entities;

namespace Restaurant.Core.Domain.Entities
{
    public class Plates : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int PeopleAmount { get; set; }
        
        //NAVEGATION PROPERTIES

        public ICollection<PlateOrders>? PlateOrders { get; set; }
        public int PlateCategoriesId { get; set; }
        public PlatesCategories? PlatesCategories { get; set; }
        public ICollection<PlatesIngridients>? PlatesIngridients { get; set;}
        
    }
}

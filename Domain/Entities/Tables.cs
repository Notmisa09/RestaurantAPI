using Restaurant.Core.Domain.Core;

namespace Restaurant.Core.Domain.Entities
{
    public class Tables 
    {
        public int Id { get; set; }
        public int PeopleAmount { get; set; }
        public string Description { get; set; }

        //Navegation properties
        public TableStatus? Status { get; set; }
        public int TableStatusId { get; set; }
        public ICollection<Orders>? Orders { get; set; }
    }
}

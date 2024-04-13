using Restaurant.Core.Application.Dto.Orders;

namespace Restaurant.Core.Application.Dto.Tables
{
    public class TablesBaseDto
    {
        public int Id { get; set; }
        public int PeopleAmount { get; set; }
        public string Description { get; set; }

        //NAVGATION PROPERTIES

        public string? Status { get; set; }
        public int? TableStatusId { get; set; }
        public List<int> Orders { get; set; }
        public List<string> OrdersDto { get; set;}
    }
}

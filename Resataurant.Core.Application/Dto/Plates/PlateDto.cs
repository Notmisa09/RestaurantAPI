using Restaurant.Core.Domain.Entities;

namespace Restaurant.Core.Application.Dto.Plates
{
    public class PlateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int PeopleAmount { get; set; }
        public int PlateCategoriesId { get; set; }

        //Nav property
        public string PlateCategory { get; set; }
        public List<string>? PlateIngridients { get; set; }
    }
}

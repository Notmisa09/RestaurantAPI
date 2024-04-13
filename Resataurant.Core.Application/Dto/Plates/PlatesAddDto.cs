namespace Restaurant.Core.Application.Dto.Plates
{
    public class PlatesAddDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public int PeopleAmount { get; set; }

        //Nav properties
        public int PlateCategoriesId { get; set; }
        public List<int> PlateIngridients { get; set; }
    }
}

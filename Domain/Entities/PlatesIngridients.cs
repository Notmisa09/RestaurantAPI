namespace Restaurant.Core.Domain.Entities
{
    public class PlatesIngridients
    {
        public int PlateId { get; set; }
        public Plates? Plates { get; set; }
        public int IngridientsId { get; set; }
        public Ingridients? Ingridients { get; set; }
    }
}

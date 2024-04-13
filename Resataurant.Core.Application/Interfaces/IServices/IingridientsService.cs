using Restaurant.Core.Application.Dto.Ingridients;
using Restaurant.Core.Domain.Entities;

namespace Restaurant.Core.Application.Interfaces.IServices
{
    public interface IingridientsService : IGenericService<IngridientsDto, IngridientsAddDto, Ingridients>
    {

    }
}

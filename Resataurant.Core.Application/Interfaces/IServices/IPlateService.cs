using Restaurant.Core.Application.Dto.Plates;
using Restaurant.Core.Domain.Entities;

namespace Restaurant.Core.Application.Interfaces.IServices
{
    public interface IPlateService : IGenericService<PlateDto , PlatesAddDto , Plates>
    {
        Task<List<PlateDto>> GetAllWithIncludePlateInfo();
        Task<List<PlateDto>> GetByIdWithInfo(int Id);
        Task UpdateWithIngridients(PlatesAddDto Dto);
    }
}

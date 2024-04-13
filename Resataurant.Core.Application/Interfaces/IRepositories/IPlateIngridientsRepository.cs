using Restaurant.Core.Domain.Entities;

namespace Restaurant.Core.Application.Interfaces.IRepositories
{
    public interface IPlateIngridientsRepository : IBaseRepository<PlatesIngridients>
    {
        Task RemoveAsyncRelations(int platesId);
        List<string> GetIngridients(int Id);
    }
}

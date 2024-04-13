using Restaurant.Core.Domain.Entities;

namespace Restaurant.Core.Application.Interfaces.IRepositories
{
    public interface IPlateOrdersRespository : IBaseRepository<PlateOrders>
    {
        void RemoveRelations(int Id);
        List<string> GetAllPlatesFromOrder(int Id);
    }
}

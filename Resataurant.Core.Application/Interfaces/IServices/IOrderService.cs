using Restaurant.Core.Application.Dto.Orders;
using Restaurant.Core.Domain.Entities;

namespace Restaurant.Core.Application.Interfaces.IServices
{
    public interface IOrderService : IGenericService<OrdersDto , OrderAddDto, Orders>
    {
        Task UpdatePlateFromOrders(OrderPlatesUpdate Dto);
        Task<List<OrdersDto>> GetByIdWithInclude(int Id);
        Task<List<OrdersDto>> GetAllWithInclude();
    }
}

using Restaurant.Core.Application.Dto.Orders;
using Restaurant.Core.Application.Dto.Tables;
using Restaurant.Core.Domain.Entities;

namespace Restaurant.Core.Application.Interfaces.IServices
{
    public interface ITableService : IGenericService<TablesBaseDto , TableAddDto , Tables>
    {
        Task<List<TableOrderDto>> GetOrderTablesById(int TableId);
        Task<TableUpdateDto> UpdateTableInfo(TableUpdateDto Dto);
        Task ChangeTableStatus(TableStatusDto Dto);
        Task<List<TablesBaseDto>> GetAllOrderFromTable(int Id);
        Task<List<TablesBaseDto>> GetAllWithInclude();
        Task<List<TablesBaseDto>> GetByIdWithInclude(int Id);
    }
}

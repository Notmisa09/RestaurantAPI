using AutoMapper;
using Restaurant.Core.Application.Dto.Orders;
using Restaurant.Core.Application.Dto.Tables;
using Restaurant.Core.Application.Enum;
using Restaurant.Core.Application.Interfaces.IRepositories;
using Restaurant.Core.Application.Interfaces.IServices;
using Restaurant.Core.Domain.Entities;

namespace Restaurant.Core.Application.Services
{
    public class TableService : GenericService<TablesBaseDto , TableAddDto , Tables > , ITableService
    {
        private readonly IMapper _mapper;
        private readonly ITableRepository _tableRepository;
        private readonly IOrdersRepository _orderRepository;
        private readonly IPlateOrdersRespository _plateOrderRepository;

        public TableService(IMapper mapper,
            ITableRepository tableRepository,
            IOrdersRepository orderRepository,
            IPlateOrdersRespository plateOrdersRespository) : base(mapper, tableRepository)
        {
            _plateOrderRepository = plateOrdersRespository;
            _mapper = mapper;
            _tableRepository = tableRepository;
            _orderRepository = orderRepository;
        }

        public async Task<List<TableOrderDto>> GetOrderTablesById(int TableId)
        {
            var query = await _orderRepository.GetAllWithInclude(new List<string> { "PlateOrders.Plates", "OrderStatus" });

            var result = query.Select(x => new TableOrderDto
            {
                Id = x.Id,
                Subtotal = x.Subtotal,
                OrderStatusId = x.OrderStatusId,
                OrderStatus = x.OrderStatus.Description,
                TableId = x.TableId,
                Table = x.Table.Description,
                Plates = x.PlateOrders.Select(x => x.Plates.Name).ToList(),
            }).Where(x => x.TableId == TableId && x.OrderStatusId == (int)OrderStatusEnum.Inprocess).ToList();
            
            return result;
        }


        public async Task<List<TablesBaseDto>> GetAllWithInclude()
        {
            var tables = await _tableRepository.GetAllWithInclude(new List<string> { "Status" });
            return tables.Select(x => new TablesBaseDto
            {
                Id = x.Id,
                Description = x.Description,
                PeopleAmount = x.PeopleAmount,
                TableStatusId = x.TableStatusId,
                Status = x.Status.Description,
            }).ToList();
        }

        public async Task<List<TablesBaseDto>> GetByIdWithInclude(int Id)
        {
            var tables = await _tableRepository.GetAllWithInclude(new List<string> { "Status" });
            return tables.Where(x => x.Id == Id).Select(x => new TablesBaseDto
            {
                Id = x.Id,
                Description = x.Description,
                PeopleAmount = x.PeopleAmount,
                TableStatusId = x.TableStatusId,
                Status = x.Status.Description,
            }).ToList();
        }

        public async Task<List<TablesBaseDto>> GetAllOrderFromTable(int Id)
        {
            var table = await GetById(Id);
            var plateorders = await _orderRepository.GetAllAsync();
            return plateorders.Where(x => x.TableId == Id ).Select(x => new TablesBaseDto
            {
                Id = table.Id,
                TableStatusId = table.TableStatusId,
                Description = table.Description,
                Orders = table.Orders,
                Status = table.Status,

            }).ToList();
        }

        public async Task<TableUpdateDto> UpdateTableInfo(TableUpdateDto Dto)
        {
            var table = await _tableRepository.GetByIdAsync(Dto.Id);
            Tables tableentity = new Tables()
            {
                Id = table.Id,
                Description = Dto.Description,
                PeopleAmount = table.PeopleAmount,
                TableStatusId = table.TableStatusId,
            };
            await _tableRepository.UpdateAsync(tableentity, tableentity.Id);
            return Dto;
        }

        public async Task ChangeTableStatus(TableStatusDto Dto)
        {
            var table = await _tableRepository.GetByIdAsync(Dto.Id);
            Tables tableentity = new Tables()
            {
                Id = table.Id,
                TableStatusId = Dto.TableStatus,
                Description = table.Description,
                PeopleAmount = table.PeopleAmount,
            };
            await _tableRepository.UpdateAsync(tableentity, tableentity.Id);
        }
    }
}

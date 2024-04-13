using AutoMapper;
using Restaurant.Core.Application.Dto.Orders;
using Restaurant.Core.Application.Interfaces.IRepositories;
using Restaurant.Core.Application.Interfaces.IServices;
using Restaurant.Core.Domain.Entities;

namespace Restaurant.Core.Application.Services
{
    public class OrderService : GenericService<OrdersDto, OrderAddDto, Orders>, IOrderService
    {
        private readonly IPlateOrdersRespository _plateOrderRepository;
        private readonly IOrdersRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderService(IMapper mapper,
                             //
                            IOrdersRepository orderRepository,
                            //
                            IPlateOrdersRespository plateOrderRepository) : base(mapper, orderRepository)
        {
            _plateOrderRepository = plateOrderRepository;
            _mapper = mapper;
            _orderRepository = orderRepository;
        }


        public override Task Remove(int Id)
        {
            _plateOrderRepository.RemoveRelations(Id);
            return base.Remove(Id);
        }

        public async Task UpdatePlateFromOrders(OrderPlatesUpdate Dto)
        {
            _plateOrderRepository.RemoveRelations(Dto.Id);
            foreach (var plates in Dto.PlateList)
            {
                PlateOrders plateorders = new()
                {
                    OrderId = Dto.Id,
                    PlateId = plates
                };
                await _plateOrderRepository.CreateAsync(plateorders);
            }
        }

        public async Task<List<OrdersDto>> GetAllWithInclude()
        {
            var orders = await _orderRepository.GetAllWithInclude(new List<string> { "Table" , "OrderStatus" });
            return orders.Select(x => new OrdersDto
            {
                Id = x.Id,
                Subtotal = x.Subtotal,
                TableId = x.TableId,
                Table = x.Table.Description,
                Plates = _plateOrderRepository.GetAllPlatesFromOrder(x.Id),
                OrderStatusId = x.OrderStatusId,
                OrderStatus = x.OrderStatus.Description
            }).ToList();
        }

        public async Task<List<OrdersDto>> GetByIdWithInclude(int Id)
        {
            var orders = await _orderRepository.GetAllWithInclude(new List<string> { "Table", "OrderStatus" });
            return orders.Where(x => x.Id == Id).Select(x => new OrdersDto
            {
                Id = x.Id,
                Subtotal = x.Subtotal,
                TableId = x.TableId,
                Table = x.Table.Description,
                Plates = _plateOrderRepository.GetAllPlatesFromOrder(x.Id),
                OrderStatusId = x.OrderStatusId,
                OrderStatus = x.OrderStatus.Description
            }).ToList();
        }

        public override async Task<OrderAddDto> Add(OrderAddDto Dtoorders)
        {
            var order = _mapper.Map<OrderAddDto>(Dtoorders);  
            var orderId = await base.Add(order);
            foreach (var item in Dtoorders.PlateOrders)
            {
                PlateOrders plateorders = new()
                {
                    OrderId = orderId.Id,
                    PlateId = item
                };
                await _plateOrderRepository.CreateAsync(plateorders);
            }
            return Dtoorders;
        }
    }
}

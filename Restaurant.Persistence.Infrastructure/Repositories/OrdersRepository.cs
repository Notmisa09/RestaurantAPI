using Microsoft.EntityFrameworkCore;
using Persistance.Restaurant.Infrastructure.Context;
using Persistence.Restaurant.Infrastructure.Repository;
using Restaurant.Core.Application.Interfaces.IRepositories;
using Restaurant.Core.Domain.Entities;
using Restaurant.Core.Application.Enum;

namespace Restaurant.Persistance.Infrastructure.Repositores
{
    public class OrdersRepository : BaseRepository<Orders>, IOrdersRepository
    {
        private readonly RestContext _context;
        public OrdersRepository(RestContext context) : base(context) { _context = context; }

        public override Task<Orders> CreateAsync(Orders entity)
        {
            entity.OrderStatusId = (int)OrderStatusEnum.Inprocess;
            return base.CreateAsync(entity);
        }
    }
}

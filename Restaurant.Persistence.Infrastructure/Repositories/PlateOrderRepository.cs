using Persistance.Restaurant.Infrastructure.Context;
using Persistence.Restaurant.Infrastructure.Repository;
using Restaurant.Core.Application.Interfaces.IRepositories;
using Restaurant.Core.Domain.Entities;

namespace Persistance.Restaurant.Infrastructure.Repositories
{
    public class PlateOrderRepository : BaseRepository<PlateOrders>, IPlateOrdersRespository
    {
        private readonly RestContext _context;
        public PlateOrderRepository(RestContext context) : base(context) { _context = context; }

        public List<string> GetAllPlatesFromOrder(int Id)
        {
            var list = _context.PlateOrders.Where(p => p.OrderId == Id)
                .Select(p => p.Plates.Name).ToList();

            return list;
        }

        public void RemoveRelations(int Id)
        {
            var orderplates = _context.PlateOrders.Where(x => x.OrderId == Id).ToList();
            _context.RemoveRange(orderplates);
        }
    }
}

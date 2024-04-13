using Persistance.Restaurant.Infrastructure.Context;
using Persistence.Restaurant.Infrastructure.Repository;
using Restaurant.Core.Application.Interfaces.IRepositories;
using Restaurant.Core.Domain.Entities;
using Restaurant.Core.Application.Enum;

namespace Restaurant.Persistance.Infrastructure.Repositores
{
    public class TablesRepository : BaseRepository<Tables> , ITableRepository
    {
        private readonly RestContext _context;
        public TablesRepository(RestContext context) : base(context) { _context = context; }

        public override Task<Tables> CreateAsync(Tables entity)
        {
            entity.TableStatusId = (int)TableStatusEnum.Available;
            return base.CreateAsync(entity);
        }
    }
}

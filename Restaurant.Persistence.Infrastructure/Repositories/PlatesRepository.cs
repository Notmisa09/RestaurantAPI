using Persistance.Restaurant.Infrastructure.Context;
using Persistence.Restaurant.Infrastructure.Repository;
using Restaurant.Core.Application.Interfaces.IRepositories;
using Restaurant.Core.Domain.Entities;

namespace Restaurant.Persistance.Infrastructure.Repositores
{
    public class PlatesRepository : BaseRepository<Plates> , IPlatesRepository
    {
        private readonly RestContext _context;
        public PlatesRepository(RestContext context) : base(context) { _context = context; }

    }
}

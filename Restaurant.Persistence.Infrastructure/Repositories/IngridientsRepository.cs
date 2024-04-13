using Persistance.Restaurant.Infrastructure.Context;
using Persistence.Restaurant.Infrastructure.Repository;
using Restaurant.Core.Application.Interfaces.IRepositories;
using Restaurant.Core.Domain.Entities;

namespace Persistance.Restaurant.Infrastructure.Repositories
{
    public class IngridientsRepository : BaseRepository<Ingridients>, IingridientsRepository
    {
        private readonly RestContext _context;
        public IngridientsRepository(RestContext context) : base(context)
        {
            _context = context;
        }
    }
}

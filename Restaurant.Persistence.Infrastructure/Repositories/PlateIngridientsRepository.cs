using Microsoft.EntityFrameworkCore;
using Persistance.Restaurant.Infrastructure.Context;
using Persistence.Restaurant.Infrastructure.Repository;
using Restaurant.Core.Application.Interfaces.IRepositories;
using Restaurant.Core.Domain.Entities;

namespace Persistance.Restaurant.Infrastructure.Repositories
{
    public class PlateIngridientsRepository : BaseRepository<PlatesIngridients>, IPlateIngridientsRepository
    {
        private readonly RestContext _context;
        public PlateIngridientsRepository(RestContext context) : base(context) { _context = context; }

        public List<string> GetIngridients(int Id)
        {
            var list = _context.PlatesIngridients.Where(a => a.PlateId == Id)
                .Select(a => a.Ingridients.Nombre).ToList();

            return list;
        }

        public async Task RemoveAsyncRelations(int platesId)
        {
            var plates = await _context.PlatesIngridients.Where(x => x.PlateId == platesId).ToListAsync();
            foreach (var item in plates)
            {
                _context.PlatesIngridients.Remove(item);
            }
            await _context.SaveChangesAsync();
        }

    }
}

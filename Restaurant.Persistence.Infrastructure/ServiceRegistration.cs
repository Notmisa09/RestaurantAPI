using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistance.Restaurant.Infrastructure.Context;
using Persistance.Restaurant.Infrastructure.Repositories;
using Restaurant.Core.Application.Interfaces.IRepositories;
using Restaurant.Persistance.Infrastructure.Repositores;

namespace Restaurant.Persistance.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureLayer(this IServiceCollection service, IConfiguration configuration)
        {

            #region Context
            if (configuration.GetValue<bool>("InMemoryDatabase"))
            {
                service.AddDbContext<RestContext>(options => options.UseInMemoryDatabase("UseInMemoryDatabase"));
            }
            else
            {
                service.AddDbContext<RestContext>(options => options
                .UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                m => m.MigrationsAssembly(typeof(RestContext).Assembly.FullName)));
            }
            #endregion

            #region Injections
            service.AddTransient<IPlatesRepository,PlatesRepository>();
            service.AddTransient<ITableRepository,TablesRepository>();
            service.AddTransient<IingridientsRepository, IngridientsRepository>();
            service.AddTransient<IOrdersRepository, OrdersRepository>();
            service.AddTransient<IPlateOrdersRespository, PlateOrderRepository>();
            service.AddTransient<IPlateIngridientsRepository, PlateIngridientsRepository>();
            #endregion

        }
    }
}

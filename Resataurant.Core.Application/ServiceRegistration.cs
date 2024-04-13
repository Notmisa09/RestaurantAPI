using Microsoft.Extensions.DependencyInjection;
using Restaurant.Core.Application.Interfaces.IServices;
using Restaurant.Core.Application.Services;
using System.Reflection;

namespace Restaurant.Core.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            #region Services
            services.AddTransient(typeof(IGenericService<,,>), typeof(GenericService<,,>));
            services.AddTransient<IPlateService, PlateService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<ITableService, TableService>();
            services.AddTransient<IingridientsService, IngridientsService>();
            #endregion
        }
    }
}

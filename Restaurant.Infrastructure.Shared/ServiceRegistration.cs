using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.Core.Application.Interfaces.IServices;
using Restaurant.Core.Domain.Settings;
using Restaurant.Infrastructure.Shared.Services;

namespace Restaurant.Infrastructure.Shared
{
    public static class ServiceRegistration
    {
        public static void AddSharedLayer(this IServiceCollection service , IConfiguration configuration)
        {
            service.Configure<MailSettings>(configuration.GetSection("MailSettings"));
            service.AddTransient<IEmailService, EmailService>();
        }
    }
}

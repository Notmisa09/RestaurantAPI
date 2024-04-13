using Microsoft.AspNetCore.Builder;

namespace Restaurant.Presentation.API.Extensions
{
    public static class AppExtensions
    {
        public static void UserSwaggerExtensions(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(optios =>
            {
                optios.SwaggerEndpoint("/swagger/v1/swagger.json", "Restaurant");

            });
        }
    }
}

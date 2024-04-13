using Microsoft.OpenApi.Models;

namespace Restaurant.Presentation.API.Extensions
{
    public static class ServiceExtension
    {
        public static void AddSwaggerExtension(this IServiceCollection service)
        {
            service.AddSwaggerGen(options =>
            {
                List<string> xmlFiles = Directory.GetFiles
                (AppContext.BaseDirectory, "*.xml", searchOption: SearchOption.TopDirectoryOnly).ToList();
                xmlFiles.ForEach(xmlFile => options.IncludeXmlComments(xmlFile));

                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "RestaurantAPI",
                    Description = "This Api is for restaurant management",
                    Contact = new OpenApiContact
                    {
                        Name = "Felix Misael Mora",
                        Email = "mismoara03@gmail.com",
                        Url = new Uri("https://www.instagram.com/misamorasuero/")
                    }
                });

                options.DescribeAllParametersInCamelCase();
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Description = "Input your Bearer token in this format - Bearer {your token here}"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                            Scheme = "Bearer",
                            Name = "Berarer",
                            In = ParameterLocation.Header,
                        }, new List<string>()
                    },
                });

            });
        }

        #region APIVERSIONING
        public static void AddApiVersioningExtension(this IServiceCollection service)
        {
            service.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });
        }

        #endregion
    }
}

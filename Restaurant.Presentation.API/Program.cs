using Microsoft.AspNetCore.Identity;
using Restaurant.Persistance.Identity;
using Restaurant.Persistance.Identity.Seeds;
using Restaurant.Persistance.Infrastructure;
using Restaurant.Core.Application;
using Restaurant.Presentation.API.Extensions;
using Restaurant.Infrastructure.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationLayer();
builder.Services.AddIdentityLayer(builder.Configuration);
builder.Services.AddInfrastructureLayer(builder.Configuration);
builder.Services.AddSharedLayer(builder.Configuration);
builder.Services.AddApiVersioningExtension();
builder.Services.AddSwaggerExtension();
builder.Services.AddHealthChecks();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSession();


var app = builder.Build();

using (var scope  = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

	try
	{
		var userManager = services.GetRequiredService<UserManager<AppUser>>();
		var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

		await DefaultRoles.SeedAsync(userManager, roleManager);
		await WaiterUser.SeedAsync(userManager, roleManager);
		await AdminUser.SeedAsync(userManager, roleManager);
		await SuperAdminUser.SeedAsync(userManager, roleManager);

	}
	catch (Exception ex)
	{

	}
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UserSwaggerExtensions();
app.UseHealthChecks("/health");
app.UseSession();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.MapControllers();

app.Run();

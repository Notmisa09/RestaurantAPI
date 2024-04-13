using Microsoft.AspNetCore.Identity;
using Restaurant.Core.Application.Enum;

namespace Restaurant.Persistance.Identity.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(UserManager<AppUser> userManager , RoleManager<IdentityRole> roleManager)
        {
           await roleManager.CreateAsync(new IdentityRole(RolesEnum.SuperAdmin.ToString()));
           await roleManager.CreateAsync(new IdentityRole(RolesEnum.Admin.ToString()));
           await roleManager.CreateAsync(new IdentityRole(RolesEnum.Waiter.ToString()));
        }
    }
}

using Microsoft.AspNetCore.Identity;
using Restaurant.Core.Application.Enum;

namespace Restaurant.Persistance.Identity.Seeds
{
    public static class SuperAdminUser
    {
        public static async Task SeedAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            AppUser superadmin = new();
            superadmin.Name = "super";
            superadmin.LastName = "admin";
            superadmin.Email = "superadmin@email.com";
            superadmin.UserName = "superadmin";
            superadmin.PhoneNumberConfirmed = true;
            superadmin.EmailConfirmed = true;

            if (userManager.Users.All(u => u.Id != superadmin.Id))
            {
                var users = await userManager.FindByEmailAsync(superadmin.Email);
                if (users == null)
                {
                    await userManager.CreateAsync(superadmin, "123Pa$$word");
                    await userManager.AddToRoleAsync(superadmin, RolesEnum.SuperAdmin.ToString());
                    await userManager.AddToRoleAsync(superadmin, RolesEnum.Admin.ToString());
                    await userManager.AddToRoleAsync(superadmin, RolesEnum.Waiter.ToString());
                }
            }

        }
    }
}

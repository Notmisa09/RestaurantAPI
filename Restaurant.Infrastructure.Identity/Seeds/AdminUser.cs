using Microsoft.AspNetCore.Identity;
using Restaurant.Core.Application.Enum;

namespace Restaurant.Persistance.Identity.Seeds
{
    public static class AdminUser
    {
        public static async Task SeedAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            AppUser adminuser = new();
            adminuser.UserName = "adminuser";
            adminuser.Email = "adminuser@email.com";
            adminuser.Name = "admin";
            adminuser.LastName = "user";
            adminuser.PhoneNumber = "829-123-9811";
            adminuser.EmailConfirmed = true;
            adminuser.PhoneNumberConfirmed = true;

            if(userManager.Users.All(u => u.Id == adminuser.Id))
            {
                var user = await userManager.FindByEmailAsync(adminuser.Email);
                if(user == null)
                {
                    await userManager.CreateAsync(adminuser, "123Pa$$word");
                    await userManager.AddToRoleAsync(adminuser, RolesEnum.Admin.ToString());
                }
            }
        }
    }
}

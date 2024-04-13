using Microsoft.AspNetCore.Identity;
using Restaurant.Core.Application.Enum;

namespace Restaurant.Persistance.Identity.Seeds
{
    public static class WaiterUser
    {
        public static async Task SeedAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            AppUser waiteruser = new();
            waiteruser.UserName = "waiteruser";
            waiteruser.Name = "waiter";
            waiteruser.LastName = "user";
            waiteruser.PhoneNumber = "809-115-1941";
            waiteruser.PhoneNumberConfirmed = true;
            waiteruser.Email = "waiter@emaiL.com";
            waiteruser.EmailConfirmed = true;

            if (userManager.Users.All(u => u.Id != waiteruser.Id))
            {
                var user = await userManager.FindByEmailAsync(waiteruser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(waiteruser, "123Pa$$word");
                    await userManager.AddToRoleAsync(waiteruser, RolesEnum.Waiter.ToString());
                }
            }
        }
    }
}

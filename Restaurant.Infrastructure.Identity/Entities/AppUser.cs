using Microsoft.AspNetCore.Identity;

namespace Restaurant.Persistance.Identity
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string LastName { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Restaurant.Persistance.Identity
{
    public class RestaurantIdentityContext : IdentityDbContext<AppUser>
    {
        public RestaurantIdentityContext(DbContextOptions<RestaurantIdentityContext> options ) : base(options) { }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            base.OnModelCreating(mb);
            mb.HasDefaultSchema("Identity");

            mb.Entity<AppUser>(entity =>
            {
                entity.ToTable(name: "Users");
            });

            mb.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "Roles");
            });

            mb.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable(name: "UserRoles");
            });

            mb.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable(name: "UserLogins");
            });
        }
    }
}

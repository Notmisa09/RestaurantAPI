using Microsoft.EntityFrameworkCore;
using Restaurant.Core.Domain.Entities;

namespace Persistance.Restaurant.Infrastructure.Context
{
    public class RestContext : DbContext
    {
        public RestContext(DbContextOptions<RestContext> context) : base(context)
        {

        }

        public DbSet<Tables> Tables { get; set; }
        public DbSet<Plates> Plates { get; set; }
        public DbSet<Ingridients> Ingridients { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<PlatesCategories> PlatesCategories { get; set; }
        public DbSet<TableStatus> TableStatus { get; set; }
        public DbSet<PlatesIngridients> PlatesIngridients { get; set; }
        public DbSet<PlateOrders> PlateOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            base.OnModelCreating(mb);

            #region DefaultData

            mb.Entity<OrderStatus>().HasData(
                new OrderStatus { Id = 1, Description = "In process" },
                new OrderStatus { Id = 2, Description = "Completed" });

            mb.Entity<TableStatus>().HasData(
                new TableStatus { Id = 1, Description = "Available" },
                new TableStatus { Id = 2, Description = "Attention process" },
                new TableStatus { Id = 3, Description = "Attended" });

            mb.Entity<PlatesCategories>().HasData(
                new PlatesCategories { Id = 1, Description = "Entry" },
                new PlatesCategories { Id = 2, Description = "Strong Plate" },
                new PlatesCategories { Id = 3, Description = "Drinks" }
                );
            #endregion

            #region Table creation
            mb.Entity<Tables>().ToTable("Tables");
            mb.Entity<Plates>().ToTable("Plates");
            mb.Entity<Ingridients>().ToTable("Ingridients");
            mb.Entity<Orders>().ToTable("Orders");
            mb.Entity<PlatesCategories>().ToTable("PlateCategories");
            mb.Entity<TableStatus>().ToTable("TableStatus");
            mb.Entity<PlatesIngridients>().ToTable("PlateIngridients");
            mb.Entity<PlateOrders>().ToTable("PlateOrders");
            mb.Entity<OrderStatus>().ToTable("OrderStatus");
            #endregion

            #region PrimeryKeys

            mb.Entity<Tables>().HasKey(t => t.Id);
            mb.Entity<Plates>().HasKey(p => p.Id);
            mb.Entity<Orders>().HasKey(o => o.Id);
            mb.Entity<Ingridients>().HasKey(i => i.Id);
            mb.Entity<PlatesCategories>().HasKey(pc => pc.Id);
            mb.Entity<TableStatus>().HasKey(ts => ts.Id);
            mb.Entity<OrderStatus>().HasKey(ts => ts.Id);

            #endregion

            #region Relations

            //Tables
            mb.Entity<Tables>()
                .HasMany(t => t.Orders)
                .WithOne(t => t.Table)
                .HasForeignKey(t => t.TableId)
                .OnDelete(DeleteBehavior.Cascade);

            //PlateOrders
            mb.Entity<PlateOrders>()
                .HasOne(po => po.Orders)
                .WithMany(po => po.PlateOrders)
                .HasForeignKey(po => po.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            mb.Entity<PlateOrders>()
                .HasOne(po => po.Plates)
                .WithMany(po => po.PlateOrders)
                .HasForeignKey(po => po.PlateId)
                .OnDelete(DeleteBehavior.Cascade);

            mb.Entity<PlateOrders>()
                .HasKey(po => new { po.OrderId, po.PlateId });


            //PlateIngridients
            mb.Entity<PlatesIngridients>()
                .HasOne(p => p.Plates)
                .WithMany(p => p.PlatesIngridients)
                .HasForeignKey(p => p.IngridientsId)
                .OnDelete(DeleteBehavior.Cascade);

            mb.Entity<PlatesIngridients>()
                .HasOne(i => i.Ingridients)
                .WithMany(i => i.PlatesIngridients)
                .HasForeignKey(i => i.IngridientsId)
                .OnDelete(DeleteBehavior.Cascade);

            mb.Entity<PlatesIngridients>().
                HasKey(pi => new { pi.PlateId, pi.IngridientsId });

            //PlateCategories
            mb.Entity<PlatesCategories>()
                .HasMany(pc => pc.Plates)
                .WithOne(pc => pc.PlatesCategories)
                .HasForeignKey(pc => pc.PlateCategoriesId)
                .OnDelete(DeleteBehavior.Cascade);

            //Orders
            mb.Entity<OrderStatus>()
                .HasMany(os => os.Orders)
                .WithOne(os => os.OrderStatus)
                .HasForeignKey(os => os.OrderStatusId)
                .OnDelete(DeleteBehavior.Cascade);

            //TableStatus
            mb.Entity<TableStatus>()
                .HasMany(ts => ts.Tables)
                .WithOne(ts => ts.Status)
                .HasForeignKey(ts => ts.TableStatusId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

        }
    }
}

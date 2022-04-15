using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class CoreApiContext : DbContext
    {
        public CoreApiContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<ProductCategory> ProductCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //builder
            //    .Entity<AppUser>()
            //    .HasMany(ur => ur.UserRoles)
            //    .WithOne(u => u.User)
            //    .HasForeignKey(u => u.UserId)
            //    .IsRequired();

            //builder
            //    .Entity<AppRole>()
            //    .HasMany(ur => ur.UserRoles)
            //    .WithOne(r => r.Role)
            //    .HasForeignKey(r => r.RoleId)
            //    .IsRequired();
        }
    }
}
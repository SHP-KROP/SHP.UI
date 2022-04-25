using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class OnlineShopContext : IdentityDbContext
        <
        AppUser,
        AppRole,
        int,
        IdentityUserClaim<int>,
        AppUserRole,
        IdentityUserLogin<int>,
        IdentityRoleClaim<int>,
        IdentityUserToken<int>
        >
    {
        public OnlineShopContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<ProductCategory> ProductCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder
                .Entity<Product>()
                .HasMany(pc => pc.ProductCategories)
                .WithOne(pc => pc.Product)
                .HasForeignKey(u => u.ProductId)
                .IsRequired();

            builder
                .Entity<Category>()
                .HasMany(pc => pc.ProductCategories)
                .WithOne(pc => pc.Category)
                .HasForeignKey(r => r.CategoryId)
                .IsRequired();
        }
    }
}
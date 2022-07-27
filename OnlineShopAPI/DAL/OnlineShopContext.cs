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

        public DbSet<Photo> Photos { get; set; }

        public DbSet<Like> Likes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder
                .Entity<Product>()
                .HasMany(pr => pr.ProductCategories)
                .WithOne(pc => pc.Product)
                .HasForeignKey(pc => pc.ProductId)
                .IsRequired();

            builder
                .Entity<Category>()
                .HasMany(ct => ct.ProductCategories)
                .WithOne(pc => pc.Category)
                .HasForeignKey(pc => pc.CategoryId)
                .IsRequired();

            builder
                .Entity<AppUser>()
                .HasMany(u => u.Products)
                .WithOne(p => p.User)
                .IsRequired();

            builder
                .Entity<Product>()
                .HasMany(pr => pr.Photos)
                .WithOne(photo => photo.Product)
                .IsRequired();

            builder
                .Entity<Product>()
                .HasMany(p => p.Likes)
                .WithOne(like => like.Product)
                .HasForeignKey(like => like.ProductId)
                .IsRequired();

            builder
                .Entity<AppUser>()
                .HasMany(u => u.Likes)
                .WithOne(like => like.User)
                .HasForeignKey(like => like.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
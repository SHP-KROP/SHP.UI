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

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<ProductInOrder> ProductsInOrder { get; set; }

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
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder
                .Entity<AppUser>()
                .HasMany(u => u.Likes)
                .WithOne(like => like.User)
                .HasForeignKey(like => like.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder
                .Entity<Order>()
                .HasMany(o => o.ProductsInOrder)
                .WithOne(o => o.Order)
                .HasForeignKey(x => x.OrderId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .Entity<AppUser>()
                .HasMany(u => u.Orders)
                .WithOne(p => p.User)
                .IsRequired();
        }
    }
}
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
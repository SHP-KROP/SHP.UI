using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class ProductCategoryContext : DbContext
    {
        public ProductCategoryContext(DbContextOptions options)
            : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
    }
}
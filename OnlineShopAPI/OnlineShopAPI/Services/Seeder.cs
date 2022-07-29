using DAL;
using DAL.Entities;
using OnlineShopAPI.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopAPI.Services
{
    public class Seeder : ISeeder
    {
        private readonly OnlineShopContext _context;

        public Seeder(OnlineShopContext context)
        {
            _context = context;
        }

        public async Task Seed()
        {
            if (_context.Set<Product>().Any())
            {
                return;
            }

            await _context.Set<Product>().AddRangeAsync(
                new Product
                {
                    Name = "product1",
                    Description = "descr",
                    Price = 200,
                    User = new AppUser()
                },
                new Product
                {
                    Name = "product2",
                    Description = "descr",
                    Price = 300,
                    User = new AppUser()
                }, new Product
                {
                    Name = "product3",
                    Description = "descr",
                    Price = 400,
                    User = new AppUser()
                }
                );

            await _context.SaveChangesAsync();
        }
    }
}

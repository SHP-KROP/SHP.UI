using System;
using DAL.Entities;
using GenericRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ProductRepository : DataRepository<Product>, IProductRepository
    {
        public ProductRepository(OnlineShopContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Product>> GetUserProducts(int userId)
        {
            var products = _context.Users
                .Where(x => x.Id == userId)
                .Include(x => x.Products)
                .SelectMany(x => x.Products);

            return products;
        }
        public async Task<Product> GetProductByNameAsync(string name)
        {
            var query = await FindAsync(pr => pr.Name == name);
            
            return query.FirstOrDefault();
        }

        public async Task<IEnumerable<Product>> GetProductRangeById(IEnumerable<int> ids)
        {
            var productsInRange = await _context.Set<Product>().Where(product => ids.Contains(product.Id)).ToListAsync();

            if (!productsInRange.Any())
            {
                return null;
            }

            return productsInRange;
        }

        public async Task<Product> LikeProductByUser(int userId, int productId)
        {
            var user = await _context.Users
                .Where(u => u.Id == userId)
                .Include(u => u.Likes)
                .ThenInclude(like => like.Product)
                .FirstOrDefaultAsync();

            if (user is null)
            {
                return null;
            }

            var product = await GetAsync(productId);

            if (product is null)
            {
                return null;
            }

            var existingLike = user.Likes
                .FirstOrDefault(like => like.UserId == user.Id && like.ProductId == product.Id);

            if (existingLike is not null)
            {
                return null;
            }

            var userLike = new Like
            {
                User = user,
                Product = product,
                UserId = user.Id,
                ProductId = product.Id
            };

            user.Likes.Add(userLike);

            return product;
        }

        public async Task<Product> UnlikeProductByUser(int userId, int productId)
        {
            var user = await _context.Users
                .Where(u => u.Id == userId)
                .Include(u => u.Likes)
                .ThenInclude(like => like.Product)
                .FirstOrDefaultAsync();

            if (user is null)
            {
                return null;
            }

            var product = await GetAsync(productId);

            var like = user.Likes
                .FirstOrDefault(like => like.ProductId == product.Id && like.UserId == user.Id);

            if (like is null)
            {
                return null;
            }

            _context.Set<Like>().Remove(like);

            return product;
        }
    }
}
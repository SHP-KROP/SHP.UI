using DAL.Entities;
using GenericRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ProductRepository : DataRepository<Product>, IProductRepository
    {
        public ProductRepository(
            IdentityDbContext
        <
        AppUser,
        AppRole,
        int,
        IdentityUserClaim<int>,
        AppUserRole,
        IdentityUserLogin<int>,
        IdentityRoleClaim<int>,
        IdentityUserToken<int>
        > context
            ) : base(context)
        {

        }

        public async Task<Product> GetProductByNameAsync(string name)
        {
            var query = await FindAsync(pr => pr.Name == name);
            
            return query.FirstOrDefault();
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
                .Where(like => like.UserId == user.Id && like.ProductId == product.Id)
                .FirstOrDefault();

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
                .Where(like => like.ProductId == product.Id && like.UserId == user.Id)
                .FirstOrDefault();

            if (like is null)
            {
                return null;
            }

            user.Likes.Remove(like);

            return product;
        }
    }
}
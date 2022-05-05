using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<AppUser> _userManager;

        public UserRepository(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> AddToRoleAsync(AppUser appUser, string role)
        {
            return await _userManager.AddToRoleAsync(appUser, role);
        }

        public async Task<IdentityResult> CreateUserAsync(AppUser appUser, string password)
        {
            return await _userManager.CreateAsync(appUser, password);
        }

        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            if (username is null)
            {
                return await Task.FromResult(null as AppUser);
            }

            return await _userManager.Users.FirstOrDefaultAsync(u => u.UserName.ToUpper() == username.ToUpper());
        }

        public async Task<ICollection<string>> GetUserRoles(AppUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<AppUser> FindAsync(int id)
        {
            return await _userManager.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(AppUser user)
        {
            await _userManager.UpdateAsync(user);
        }

        public async Task AddProductToUserAsync(int id, Product product)
        {
            var user = await _userManager.Users
                .Where(x => x.Id == id)
                .Include(u => u.Products)
                .FirstOrDefaultAsync();

            user.Products.Add(product);

            await _userManager.UpdateAsync(user);
        }

        public async Task<AppUser> GetUserWithProductsWithPhotosAsync(int id)
        {
            var user = await _userManager.Users
                .Where(x => x.Id == id)
                .Include(u => u.Products)
                .ThenInclude(p => p.Photos)
                .FirstOrDefaultAsync();

            return user;
        }

        public async Task<IEnumerable<Product>> GetProductsLikedByUser(int id)
        {
            var user = await _userManager.Users
                .Where(u => u.Id == id)
                .Include(u => u.Likes)
                .ThenInclude(like => like.Product)
                .FirstOrDefaultAsync();

            var products = user.Likes.Select(like => like.Product);

            return products;
        }
    }
}

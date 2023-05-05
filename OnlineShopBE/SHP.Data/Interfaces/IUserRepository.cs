using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUserRepository
    {
        public Task<AppUser> GetUserByUsernameAsync(string username);

        public Task<AppUser> GetUserByEmailAsync(string email);

        public Task<IEnumerable<AppUser>> GetUsersAsync();

        public Task<IdentityResult> CreateUserAsync(AppUser appUser, string password);

        public Task<IdentityResult> AddToRoleAsync(AppUser appUser, string role);

        public Task<AppUser> FindAsync(int id);

        public Task UpdateAsync(AppUser user);

        public Task AddProductToUserAsync(int id, Product product);

        public Task<ICollection<string>> GetUserRoles(AppUser user);

        public Task<AppUser> GetUserWithProductsWithPhotosAsync(int id);

        public Task<IEnumerable<Product>> GetProductsLikedByUser(int id);

        public void RemoveUserById(int id);
    }
}

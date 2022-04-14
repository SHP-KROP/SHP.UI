using IdentityServer.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdentityServer.Data.Interfaces
{
    public interface IUserRepository
    {
        public Task<AppUser> GetUserByUsernameAsync(string username);

        public Task<IEnumerable<AppUser>> GetUsersAsync();

        public Task<IdentityResult> CreateUserAsync(AppUser appUser, string password);

        public Task<IdentityResult> AddToRoleAsync(AppUser appUser, string role);
    }
}

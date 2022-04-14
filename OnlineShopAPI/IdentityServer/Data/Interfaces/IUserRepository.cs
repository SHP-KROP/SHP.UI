using IdentityServer.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdentityServer.Data.Interfaces
{
    public interface IUserRepository
    {
        public Task<AppUser> GetUserByUsernamyAsync(string username);

        public Task<IEnumerable<AppUser>> GetUsersAsync();

        public Task<IdentityResult> CreateUserAsync(AppUser appUser, string password);
    }
}

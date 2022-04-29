using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }
    }
}

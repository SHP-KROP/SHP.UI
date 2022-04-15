using IdentityServer.Data.Entities;
using IdentityServer.Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace IdentityServer.Data
{
    public class SignInManager : ISignInManager
    {
        private readonly SignInManager<AppUser> _signInManagerDelegated;

        public SignInManager(SignInManager<AppUser> signInManagerDelegated)
        {
            _signInManagerDelegated = signInManagerDelegated;
        }

        public async Task<SignInResult> CheckPasswordSignInAsync(AppUser user, string password, bool lockoutOnFailur)
        {
            return await _signInManagerDelegated.CheckPasswordSignInAsync(user, password, lockoutOnFailur);
        }
    }
}

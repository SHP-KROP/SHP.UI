using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ISignInManager
    {
        Task<SignInResult> CheckPasswordSignInAsync(AppUser user, string password, bool lockoutOnFailure);
    }
}

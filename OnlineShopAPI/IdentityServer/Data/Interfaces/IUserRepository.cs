using IdentityServer.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdentityServer.Data.Interfaces
{
    public interface IUserRepository
    {
        Task AddAsync(AppUser user);

        Task<IEnumerable<AppUser>> GetUsers();

        AppUser GetUserByUsername(string username);

        Task<bool> ConfirmAsync();
    }
}

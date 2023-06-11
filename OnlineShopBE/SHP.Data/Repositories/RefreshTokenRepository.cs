using DAL.Entities;
using DAL.Interfaces;
using GenericRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using DAL;

namespace SHP.Data.Repositories
{
    public class RefreshTokenRepository : DataRepository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(OnlineShopContext context) : base(context)
        {

        }

        public async Task<RefreshToken> FindByToken(string token)
        {
            var refreshToken = await _context.Set<RefreshToken>().SingleOrDefaultAsync(x => x.Token == token);

            return refreshToken;
        }
    }
}

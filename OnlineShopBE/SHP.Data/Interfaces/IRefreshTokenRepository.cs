using DAL.Entities;
using GenericRepository;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IRefreshTokenRepository : IDataRepository<RefreshToken>
    {
        Task<RefreshToken> FindByToken(string token);
    }
}

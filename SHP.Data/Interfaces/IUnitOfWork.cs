using DAL.Repositories;
using SHP.Data.Interfaces;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IProductRepository ProductRepository { get; }

        ICategoryRepository CategoryRepository { get; }

        IUserRepository UserRepository { get; }

        ISignInManager SignInManager { get; }

        IRefreshTokenRepository RefreshTokenRepository { get; }

        IOrderRepository OrderRepository { get; }

        Task<bool> ConfirmAsync();
    }
}
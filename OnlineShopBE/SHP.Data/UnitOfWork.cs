using System.Diagnostics.CodeAnalysis;
using DAL.Entities;
using DAL.Repositories;
using Microsoft.AspNetCore.Identity;
using SHP.Data.Repositories;
using System.Threading.Tasks;
using SHP.Data.Interfaces;

namespace DAL.Interfaces
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OnlineShopContext _context;
        
        public UnitOfWork(OnlineShopContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _context = context;

            _context.Database.EnsureCreated();

            ProductRepository = new ProductRepository(context);
            CategoryRepository = new CategoryRepository(context);
            UserRepository = new UserRepository(userManager, context);
            SignInManager = new SignInManager(signInManager);
            RefreshTokenRepository = new RefreshTokenRepository(context);
            OrderRepository = new OrderRepository(context);
        }

        public IProductRepository ProductRepository { get; private set; }

        public ICategoryRepository CategoryRepository { get; private set; }

        public IUserRepository UserRepository { get; private set; }

        public ISignInManager SignInManager { get; private set; }

        public IRefreshTokenRepository RefreshTokenRepository { get; private set; }

        public IOrderRepository OrderRepository { get; private set; }

        public async Task<bool> ConfirmAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}

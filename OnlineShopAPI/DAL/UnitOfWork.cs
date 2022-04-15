using DAL.Repositories;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CoreApiContext _context;
        
        public UnitOfWork(CoreApiContext context)
        {
            _context = context;

            _context.Database.EnsureCreated();

            ProductRepository = new ProductRepository(context);
            CategoryRepository = new CategoryRepository(context);
        }

        public IProductRepository ProductRepository { get; private set; }

        public ICategoryRepository CategoryRepository { get; private set; }

        public async Task<bool> ConfirmAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}

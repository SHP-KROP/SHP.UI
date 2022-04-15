using DAL.Entities;
using DAL.Repositories;
using GenericRepository;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CoreApiContext _context;
        
        public UnitOfWork(CoreApiContext context)
        {
            _context = context;

            ProductRepository = new ProductRepository(context);
            CategoryRepository = new DataRepository<Category>(context);
            ProductCategoryRepository = new DataRepository<ProductCategory>(context);

            _context.Database.EnsureCreated();
        }

        public IProductRepository ProductRepository { get; private set; }

        public IDataRepository<Category> CategoryRepository { get; private set; }

        public IDataRepository<ProductCategory> ProductCategoryRepository { get; private set; }

        public async Task<bool> ConfirmAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}

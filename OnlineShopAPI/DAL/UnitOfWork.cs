using DAL.Entities;
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

            ProductRepository = new DataRepository<Product>(context);
            CategoryRepository = new DataRepository<Category>(context);
            ProductCategoryRepository = new DataRepository<ProductCategory>(context);

            _context.Database.EnsureCreated();
        }

        public DataRepository<Product> ProductRepository { get; private set; }

        public DataRepository<Category> CategoryRepository { get; private set; }

        public DataRepository<ProductCategory> ProductCategoryRepository { get; private set; }

        public async Task<bool> ConfirmAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}

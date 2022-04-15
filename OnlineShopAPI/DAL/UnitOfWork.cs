using DAL.Entities;
using GenericRepository;

namespace DAL.Interfaces
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProductCategoryContext _context;
        public DataRepository<Product> ProductRepository { get; private set; }
        public DataRepository<Category> CategoryRepository { get; private set; }
        public DataRepository<ProductCategory> ProductCategoryRepository { get; private set; }
        public UnitOfWork(ProductCategoryContext context)
        {
            _context = context;
            ProductRepository = new DataRepository<Product>(context);
            CategoryRepository = new DataRepository<Category>(context);
            ProductCategoryRepository = new DataRepository<ProductCategory>(context);
            _context.Database.EnsureCreated();
        }
        public async Task<bool> ConfirmAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}

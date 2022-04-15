using DAL.Entities;
using GenericRepository;

namespace DAL.Interfaces
{
    public interface IUnitOfWork
    {
        DataRepository<Product> ProductRepository { get; }
        DataRepository<Category> CategoryRepository { get; }
        DataRepository<ProductCategory> ProductCategoryRepository { get; }
        Task<bool> ConfirmAsync();
    }
}

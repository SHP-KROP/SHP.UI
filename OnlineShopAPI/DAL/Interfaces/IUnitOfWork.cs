using DAL.Entities;
using GenericRepository;
using System.Threading.Tasks;

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

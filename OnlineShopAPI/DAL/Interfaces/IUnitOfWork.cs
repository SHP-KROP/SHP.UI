using DAL.Entities;
using DAL.Repositories;
using GenericRepository;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IProductRepository ProductRepository { get; }

        IDataRepository<Category> CategoryRepository { get; }

        IDataRepository<ProductCategory> ProductCategoryRepository { get; }

        Task<bool> ConfirmAsync();
    }
}

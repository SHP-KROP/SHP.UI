using DAL.Entities;
using DAL.Repositories;
using GenericRepository;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IProductRepository ProductRepository { get; }

        ICategoryRepository CategoryRepository { get; }

        Task<bool> ConfirmAsync();
    }
}
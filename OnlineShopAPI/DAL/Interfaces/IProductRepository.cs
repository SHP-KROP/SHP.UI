using DAL.Entities;
using GenericRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface IProductRepository : IDataRepository<Product>
    {
        Task<Product> GetProductByNameAsync(string name);
    }
}
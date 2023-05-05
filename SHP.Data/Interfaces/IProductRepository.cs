using DAL.Entities;
using GenericRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface IProductRepository : IDataRepository<Product>
    {
        Task<Product> GetProductByNameAsync(string name);

        Task<Product> LikeProductByUser(int userId, int productId);

        Task<Product> UnlikeProductByUser(int userId, int productId);

        Task<IEnumerable<Product>> GetProductRangeById(IEnumerable<int> ids);
    }
}
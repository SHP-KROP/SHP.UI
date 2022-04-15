using DAL.Entities;
using GenericRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface ICategoryRepository : IDataRepository<Category>
    {
        Task<IEnumerable<Product>> GetTopFiveMostPopularCategories();
    }
}
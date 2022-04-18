using DAL.Entities;
using GenericRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ProductRepository : DataRepository<Product>, IProductRepository
    {
        public ProductRepository(DbContext context) : base(context)
        {

        }

        public async Task<Product> GetProductByNameAsync(string name)
        {
            var query = await FindAsync(pr => pr.Name == name);
            
            return query.FirstOrDefault();
        }
    }
}
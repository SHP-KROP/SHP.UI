using DAL.Entities;
using GenericRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ProductRepository : DataRepository<Product>, IProductRepository
    {
        public ProductRepository(
            IdentityDbContext
        <
        AppUser,
        AppRole,
        int,
        IdentityUserClaim<int>,
        AppUserRole,
        IdentityUserLogin<int>,
        IdentityRoleClaim<int>,
        IdentityUserToken<int>
        > context
            ) : base(context)
        {

        }

        public async Task<Product> GetProductByNameAsync(string name)
        {
            var query = await FindAsync(pr => pr.Name == name);
            
            return query.FirstOrDefault();
        }
    }
}
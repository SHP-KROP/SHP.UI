using DAL.Entities;
using GenericRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class CategoryRepository : DataRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(IdentityDbContext
        <
        AppUser,
        AppRole,
        int,
        IdentityUserClaim<int>,
        AppUserRole,
        IdentityUserLogin<int>,
        IdentityRoleClaim<int>,
        IdentityUserToken<int>
        > context) : base(context)
        {

        }

        public async Task<Category> GetCategoryByName(string categoryName)
        {
            var category = await FindAsync(c => c.Name == categoryName);

            return category.FirstOrDefault();
        }
    }
}
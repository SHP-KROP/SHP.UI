using System;
using DAL.Entities;
using GenericRepository;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class CategoryRepository : DataRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(OnlineShopContext context) : base(context)
        {

        }

        public async Task<Category> GetCategoryByName(string categoryName)
        {
            var category = await FindAsync(c => c.Name == categoryName);

            return category.FirstOrDefault();
        }

        public async Task<bool> AnyAsync(Expression<Func<Category, bool>> predicate) 
            => await _context.Categories.AnyAsync(predicate);
    }
}
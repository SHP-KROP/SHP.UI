using DAL.Entities;
using GenericRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class CategoryRepository : DataRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(DbContext context) : base(context)
        {

        }

        public async Task<Category> GetCategoryByName(string categoryName)
        {
            var category = await FindAsync(c => c.Name == categoryName);

            return category.FirstOrDefault();
        }
    }
}
using DAL.Entities;
using GenericRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class CategoryRepository : DataRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(DbContext context) : base(context)
        {

        }

        public Task<IEnumerable<Product>> GetTopFiveMostPopularCategories()
        {
            throw new NotImplementedException();
        }
    }
}
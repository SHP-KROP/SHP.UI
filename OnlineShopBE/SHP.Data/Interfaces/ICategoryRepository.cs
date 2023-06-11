using System;
using System.Linq.Expressions;
using DAL.Entities;
using GenericRepository;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface ICategoryRepository : IDataRepository<Category>
    {
        Task<Category> GetCategoryByName(string categoryName);

        Task<bool> AnyAsync(Expression<Func<Category, bool>> predicate);
    }
}
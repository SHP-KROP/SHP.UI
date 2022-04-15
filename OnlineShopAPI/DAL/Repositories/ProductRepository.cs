using DAL.Entities;
using GenericRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ProductRepository : DataRepository<Product>, IProductRepository
    {
        public ProductRepository(DbContext context) : base(context)
        {

        }

        public Task<IEnumerable<Product>> GetTopFiveMostPopularProducts()
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Interfaces;

namespace SHP.OnlineShopAPI.Web.Services
{
    public sealed class Seeder
    {
        private readonly IUnitOfWork _uow;

        public Seeder(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task Seed()
        {
            var clothesCategory = new Category("Clothes");
            var shoesCategory = new Category("Shoes");
            var accessoriesCategory = new Category("Accessories");

            await AddCategoriesIfNotExist(clothesCategory, shoesCategory, accessoriesCategory);
            
            var seller = await _uow.UserRepository.GetUserByUsernameAsync("seller");

            if (seller is null)
            {
                throw new InvalidOperationException(
                    "Seller user does not exist. Run the seeding mechanism for default users");
            }

            if (seller.Products.Any())
            {
                return;
            }
            
            seller.Products.Add(new Product
            {
                Amount = 100,
                Description = "Very good T-shirt",
                IsAvailable = true,
                Name = "Polo T-shirt",
                Price = 22,
                ProductCategories = new List<ProductCategory>
                {
                    new()
                    {
                        Category = clothesCategory
                    }
                }
            });
            seller.Products.Add(new Product
            {
                Amount = 100,
                Description = "Very good Sneakers",
                IsAvailable = true,
                Name = "Gucci sneakers",
                Price = 500,
                ProductCategories = new List<ProductCategory>
                {
                    new()
                    {
                        Category = shoesCategory
                    }
                }
            });
            seller.Products.Add(new Product
            {
                Amount = 100,
                Description = "Very good Bracer",
                IsAvailable = true,
                Name = "Prada Bracer",
                Price = 200,
                ProductCategories = new List<ProductCategory>
                {
                    new()
                    {
                        Category = accessoriesCategory
                    }
                }
            });
            await _uow.ConfirmAsync();
        }

        private async Task AddCategoriesIfNotExist(params Category[] categories)
        {
            foreach (var category in categories)
            {
                if (!await _uow.CategoryRepository.AnyAsync(x => x.Name == category.Name))
                {
                    await _uow.CategoryRepository.AddAsync(category);
                    await _uow.ConfirmAsync();
                }
            }
        }
    }
}
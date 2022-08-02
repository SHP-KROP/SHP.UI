using AutoMapper;
using DAL.Entities;
using OnlineShopAPI.DTO.Category;
using OnlineShopAPI.DTO.Product;

namespace OnlineShopAPI.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Product mappings
            CreateMap<ProductDto, Product>();
            CreateMap<Product, ProductDto>();

            CreateMap<Product, CreateProductDto>();
            CreateMap<CreateProductDto, Product>();

            CreateMap<ChangeProductDto, Product>();
            CreateMap<Product, ChangeProductDto>();

            // Category mappings
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();

            CreateMap<CreateCategoryDto, Category>();
            CreateMap<Category, CreateCategoryDto>();
        }
    }
}

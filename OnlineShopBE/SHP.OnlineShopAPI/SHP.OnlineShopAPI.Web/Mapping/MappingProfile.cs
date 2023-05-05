using AutoMapper;
using DAL.Entities;
using SHP.OnlineShopAPI.Web.DTO.Category;
using SHP.OnlineShopAPI.Web.DTO.Product;

namespace SHP.OnlineShopAPI.Web.Mapping
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

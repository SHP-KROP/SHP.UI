using AutoMapper;
using DAL.Entities;
using OnlineShopAPI.DTO.Product;

namespace OnlineShopAPI.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductDto, Product>();
            CreateMap<Product, ProductDto>();

            CreateMap<Product, CreateProductDto>();
            CreateMap<CreateProductDto, Product>();

            CreateMap<ChangeProductDto, Product>();
            CreateMap<Product, ChangeProductDto>();
        }
    }
}

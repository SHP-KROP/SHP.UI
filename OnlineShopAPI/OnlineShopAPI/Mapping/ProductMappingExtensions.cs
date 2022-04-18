using DAL.Entities;
using OnlineShopAPI.DTO.Product;

namespace OnlineShopAPI.Mapping
{
    public static class ProductMappingExtensions
    {
        public static void ProjectFrom(this Product product, ChangeProductDto changeProductDto)
        {
            product.Name = changeProductDto.Name;
            product.Description = changeProductDto.Description;
            product.Price = changeProductDto.Price;
            product.IsAvailable = changeProductDto.IsAvailable;
            product.PhotoUrl = changeProductDto.PhotoUrl;
            product.Amount = changeProductDto.Amount;
        }
    }
}

using DAL.Entities;
using SHP.OnlineShopAPI.Web.DTO.Product;

namespace SHP.OnlineShopAPI.Web.Mapping
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

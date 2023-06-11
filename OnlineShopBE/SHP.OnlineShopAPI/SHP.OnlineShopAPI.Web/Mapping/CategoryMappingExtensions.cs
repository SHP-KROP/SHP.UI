using DAL.Entities;
using SHP.OnlineShopAPI.Web.DTO.Category;

namespace SHP.OnlineShopAPI.Web.Mapping
{
    public static class CategoryMappingExtensions
    {
        /// <summary>
        /// Performs non-overridable mapping without creating a new instance
        /// </summary>
        /// <param name="category">Category - destination. We map to it</param>
        /// <param name="changeCategoryDto">ChangeCategoryDTO - source. We map from it</param>
        public static void ProjectFrom(this Category category, ChangeCategoryDto changeCategoryDto)
        {
            category.Name = changeCategoryDto.Name;
        }
    }
}

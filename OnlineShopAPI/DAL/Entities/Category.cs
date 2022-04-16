using System.Collections.Generic;

namespace DAL.Entities
{
    public class Category
    {
        public int Id { get; set; }

        public string CategoryName { get; set; }

        public ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
using System.Collections.Generic;

namespace DAL.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double Amount { get; set; }

        public double Price { get; set; }

        public bool IsAvaliable { get; set; }

        public string PhotoUrl { get; set; }

        public string Description { get; set; }

        public ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
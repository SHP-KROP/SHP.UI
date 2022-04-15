using System.Collections.Generic;

namespace DAL.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Amount { get; set; }

        public double Price { get; set; }

        public bool IsAvailable { get; set; }

        public string PhotoUrl { get; set; }

        public string Description { get; set; }

        public ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
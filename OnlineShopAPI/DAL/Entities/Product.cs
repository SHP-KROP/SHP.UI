using System.Collections.Generic;

namespace DAL.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double Amount { get; set; }

        public double Price { get; set; }

        public bool IsAvailable { get; set; }

        public string PhotoUrl { get; set; }

        public string Description { get; set; }

        public ICollection<ProductCategory> ProductCategories { get; set; }

        public ICollection<Photo> Photos { get; set; }

        public ICollection<Like> Likes { get; set; }

        public AppUser User { get; set; }
    }
}
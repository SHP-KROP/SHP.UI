using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    /// <summary>
    /// Represents a product order which has been done by a Buyer
    /// </summary>
    public class Order
    {
        public int Id { get; set; }
        
        public int UserId { get; set; }

        public AppUser User { get; set; }

        public ICollection<ProductInOrder> ProductsInOrder { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
    }
}
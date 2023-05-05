using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class AppUser : IdentityUser<int>
    {
        [Required]
        public string Gender { get; set; } = "Male";

        [Required]
        [Range(0, 10000)]
        public double Balance { get; set; }

        public string PhotoUrl { get; set; }

        public ICollection<AppUserRole> UserRoles { get; set; }

        public ICollection<Product> Products { get; set; }

        public ICollection<Like> Likes { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Data.Entities
{
    public class AppUser : IdentityUser<int>
    {
        [Required]
        public string Gender { get; set; } = "Male";
        
        [Required]
        [Range(0, 10000)]
        public double Balance { get; set; }

        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace IdentityServer.Data.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public string Gender { get; set; }
        
        public double Balance { get; set; }

        public ICollection<AppUserRole> Roles { get; set; }
    }
}

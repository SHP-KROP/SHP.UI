using IdentityServer.Data;
using IdentityServer.Data.Entities;
using IdentityServer.Helpers.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace IdentityServer.Helpers
{
    public class Seeder : ISeeder
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public Seeder(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public Seeder()
        {

        }

        public void Seed()
        {
            var roles = new List<AppRole>
            {
                new AppRole { Name = "admin" },
                new AppRole { Name = "moderator" },
                new AppRole { Name = "seller" },
                new AppRole { Name = "buyer" }
            };

            foreach (AppRole role in roles)
            {
                _roleManager.CreateAsync(role);
            }

            var admin = new AppUser { UserName = "admin" };
            var moder = new AppUser { UserName = "moder" };
            var seller = new AppUser { UserName = "seller" };
            var buyer = new AppUser { UserName = "buyer" };

            _userManager.CreateAsync(admin, "Pa$$w0rd");
            _userManager.CreateAsync(moder, "Pa$$w0rd");
            _userManager.CreateAsync(seller, "Pa$$w0rd");
            _userManager.CreateAsync(buyer, "Pa$$w0rd");

            _userManager.AddToRoleAsync(admin, "admin");
            _userManager.AddToRoleAsync(moder, "moderator");
            _userManager.AddToRoleAsync(seller, "seller");
            _userManager.AddToRoleAsync(buyer, "buyer");
        }
    }
}

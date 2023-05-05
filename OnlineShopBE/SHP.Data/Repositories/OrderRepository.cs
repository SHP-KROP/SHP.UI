using DAL.Entities;
using GenericRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SHP.Data.Interfaces;

namespace SHP.Data.Repositories
{
    public class OrderRepository : DataRepository<Order>, IOrderRepository
    {
        public OrderRepository(IdentityDbContext
        <
            AppUser,
            AppRole,
            int,
            IdentityUserClaim<int>,
            AppUserRole,
            IdentityUserLogin<int>,
            IdentityRoleClaim<int>,
            IdentityUserToken<int>
        > context) : base(context)
        {

        }
    }
}
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace SHP.OnlineShopAPI.Web.Controllers
{
    public static class ControllerExtensions
    {
        public const int NotExistingUserId = -1;

        public static int GetUserId(this ControllerBase controller)
        {
            return int.TryParse(controller.User?.Claims?
                .First(x => x.Type == ClaimTypes.NameIdentifier)?.Value, out var id)
                ? id
                : NotExistingUserId;
        }
    }
}

using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SHP.OnlineShopAPI.Web.Constants;
using SHP.OnlineShopAPI.Web.DTO.Checkout;
using SHP.OnlineShopAPI.Web.Services.Interfaces;

namespace SHP.OnlineShopAPI.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = Roles.AdminOrModerOrBuyer)]
    public sealed class CheckoutController : ControllerBase
    {
        private readonly ICheckoutService _checkoutService;

        public CheckoutController(ICheckoutService checkoutService)
        {
            _checkoutService = checkoutService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Checkout([FromBody] CheckoutDto checkoutDto)
        {
            var checkoutResult = await _checkoutService.Checkout(checkoutDto.ProductsInBasket, checkoutDto.CreditCard);

            return checkoutResult 
                ? Ok("Checkout successful") 
                : BadRequest("Unable to checkout");
        }
    }
}
using System.Collections.Generic;

namespace SHP.OnlineShopAPI.Web.DTO.Checkout
{
    public sealed record CheckoutDto
    (
        CreditCardDto CreditCard, 
        IEnumerable<ProductInBasketDto> ProductsInBasket
    );
}
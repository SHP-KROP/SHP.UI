namespace SHP.OnlineShopAPI.Web.DTO.Checkout
{
    public sealed record CreditCardDto
    (
        string Number,
        string ExpirationMonth,
        string ExpirationYear,
        string Cvc
    );
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Interfaces;
using Microsoft.Extensions.Logging;
using SHP.OnlineShopAPI.Web.DTO.Checkout;
using SHP.OnlineShopAPI.Web.Services.Interfaces;
using Stripe;

namespace SHP.OnlineShopAPI.Web.Services
{
    public sealed class CheckoutService : ICheckoutService
    {
        private const string PaymentCurrency = "USD";
        private const string PaymentDescription = "Purchase in Online Shop";
        private const int PenniesInOneDollar = 100;

        private readonly TokenService _tokenService = new();
        private ChargeService _chargeService = new();
        
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CheckoutService> _logger;

        public CheckoutService(IUnitOfWork unitOfWork, ILogger<CheckoutService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        
        public async Task<bool> Checkout(IEnumerable<ProductInBasketDto> productsInBasket, CreditCardDto card)
        {
            _logger.LogInformation("Started processing checkout...");

            var productIds = productsInBasket.Select(x => x.ProductId);
            
            var products = await _unitOfWork.ProductRepository.GetProductRangeById(productIds);

            var totalPrice = products
                .OrderBy(x => x.Id)
                .Zip(productsInBasket
                    .OrderBy(x => x.ProductId))
                .Sum(x => x.Second.Amount * x.First.Price * PenniesInOneDollar);
            
            _logger.LogInformation($"Basket contains products: {string.Join(',', productIds)}");
            _logger.LogInformation($"Total price for the basket is {totalPrice / PenniesInOneDollar} {PaymentCurrency}");

            try
            {
                var tokenCreateOptions = CreateTokenCreateOptionsFromCard();

                var stripeToken = await _tokenService.CreateAsync(tokenCreateOptions);

                var chargeCreateOptions = new ChargeCreateOptions
                {
                    Amount = (long)totalPrice,
                    Currency = PaymentCurrency,
                    Description = PaymentDescription,
                    Source = stripeToken.Id
                };

                var charge = await _chargeService.CreateAsync(chargeCreateOptions);

                return charge.Paid;
            }
            catch (StripeException ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected exception happened during a Stripe charge");
                throw;
            }

            TokenCreateOptions CreateTokenCreateOptionsFromCard()
            {
                return new TokenCreateOptions
                {
                    Card = new TokenCardOptions()
                    {
                        Number = card.Number,
                        ExpYear = card.ExpirationYear,
                        ExpMonth = card.ExpirationMonth,
                        Cvc = card.Cvc
                    }
                };
            }
        }
    }
}
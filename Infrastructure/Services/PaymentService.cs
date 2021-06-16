using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Services;
using Domain.Dtos;
using Stripe.Checkout;

namespace Infrastructure.Services
{
    public class PaymentService : IPaymentService
    {
        private const string successPaymentUrl = "http://localhost:4200/success";
        private const string cancelPaymentUrl = "http://localhost:4200/cancel";
        public async Task<Session> CreatePaymentSessionAsync(PaymentRequest paymentRequest)
        {
            var sessionService = new SessionService();

            var options = CreateOptions(paymentRequest.Value);

            var session = await sessionService.CreateAsync(options);

            return session;
        }

        private SessionCreateOptions CreateOptions(int value)
        {
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
                {
                    "card"
                },
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = value,
                            Currency = "pln",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "Donation"
                            }
                        },
                        Quantity = 1
                    }
                },
                Mode = "payment",
                SuccessUrl = successPaymentUrl,
                CancelUrl = cancelPaymentUrl,
            };

            return options;
        }
    }
}
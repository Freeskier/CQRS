using System.Threading.Tasks;
using Domain.Dtos;
using Stripe.Checkout;

namespace Application.Services
{
    public interface IPaymentService
    {
        Task<Session> CreatePaymentSessionAsync(PaymentRequest paymentRequest);
    }
}
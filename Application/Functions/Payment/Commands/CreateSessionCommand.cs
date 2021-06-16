using Domain.Dtos;
using MediatR;
using Stripe.Checkout;

namespace Application.Functions.Payment.Commands
{
    public class CreateSessionCommand : IRequest<Session>
    {
        public PaymentRequest Payment { get; set; }
    }
}
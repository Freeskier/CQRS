using System.Threading;
using System.Threading.Tasks;
using Application.Services;
using MediatR;
using Stripe.Checkout;

namespace Application.Functions.Payment.Commands
{
    public class CreateSessionCommandHandler : IRequestHandler<CreateSessionCommand, Session>
    {
        private readonly IPaymentService _paymentService;
        public CreateSessionCommandHandler(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        public async Task<Session> Handle(CreateSessionCommand request, CancellationToken cancellationToken)
        {
            var session = await _paymentService.CreatePaymentSessionAsync(request.Payment);

            return session;
        }
    }
}
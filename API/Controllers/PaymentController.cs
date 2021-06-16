using System.Threading.Tasks;
using Application.Services;
using Domain.Dtos;
using Application.Functions.Payment.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PaymentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePaymentSession(PaymentRequest request)
        {
            var command = new CreateSessionCommand
            {
                Payment = request
            };

            var session = await _mediator.Send(command);

            return Ok(session);
        }
    }
}
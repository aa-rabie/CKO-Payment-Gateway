using CKO.PaymentGateway.Application.Features.Payments.Commands.CreatePayment;
using CKO.PaymentGateway.Application.Features.Payments.Queries.GetPayment;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CKO.PaymentGateway.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PaymentsController(IMediator mediator)
        {
            _mediator = mediator;
        }
       
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetPaymentQuery()
            {
                Id = id
            };

            var dto = await _mediator.Send(query);
            return Ok(dto);
        }

        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePaymentCommand request)
        {
            var response = await _mediator.Send(request);
            if (response.Success)
            {
                return CreatedAtAction(nameof(GetById), new { id = response.PaymentId }, response);
            }

            return UnprocessableEntity(response);
        }
    }
}

using ECommerceSystem.Application.Commands.CreateOrder;
using ECommerceSystem.Application.Queries.GetOrders;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var query = new GetOrdersQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}

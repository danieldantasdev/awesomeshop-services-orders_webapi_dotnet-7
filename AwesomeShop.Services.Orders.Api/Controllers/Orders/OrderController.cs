using AwesomeShop.Services.Orders.Application.Commands.Orders;
using AwesomeShop.Services.Orders.Application.Queries.Orders;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AwesomeShop.Services.Orders.Api.Controllers.Orders;

[Route(("api/customer/{customerId}/orders"))]
// [Route(("api/orders"))]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrderController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var query = new GetOrderByIdQueryInputModel(id);
        var result = await _mediator.Send(query);
        if (result == null)
            NotFound();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AddOrderCommandInputModel addOrderCommandInputModel)
    {
        var id = await _mediator.Send(addOrderCommandInputModel);
        return CreatedAtAction(nameof(Get), new { id = id }, addOrderCommandInputModel);

    }
}
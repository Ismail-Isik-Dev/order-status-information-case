using Microsoft.AspNetCore.Mvc;
using Ordering.Services.Contracts;
using Ordering.Dtos;
using Ordering.Dtos.Mappings;

namespace Ordering.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<ActionResult<OrderCreateResponseDto>> Create([FromBody] OrderCreateDto order)
        {
            var response = await _orderService.CreateOrderAsync(order.ToOrder());

            return Ok(response);
        }
    }
}

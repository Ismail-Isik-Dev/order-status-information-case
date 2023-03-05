using Microsoft.AspNetCore.Mvc;
using Ordering.Services.Contracts;
using Ordering.Dtos;

namespace Ordering.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<StatusController> _logger;

        public StatusController(IOrderService orderService, ILogger<StatusController> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        [HttpPatch]
        public async Task<ActionResult<OrderStatusChangeResponseDto>> ChangeStatus([FromBody] OrderChangeStatusDto order)
        {
            var result = await _orderService.ChangeOrderStatusAsync(order);

            return Ok(result);
        }
    }
}

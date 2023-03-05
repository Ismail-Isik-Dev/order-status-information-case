using Ordering.Dtos;
using Ordering.Entities;

namespace Ordering.Services.Contracts
{
    public interface IOrderService
    {
        Task<OrderCreateResponseDto> CreateOrderAsync(Order order);
        Task<OrderStatusChangeResponseDto> ChangeOrderStatusAsync(OrderChangeStatusDto order);
    }
}

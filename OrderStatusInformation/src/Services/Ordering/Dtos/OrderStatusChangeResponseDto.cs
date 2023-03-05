using Ordering.Comman;
using Ordering.Entities;

namespace Ordering.Dtos
{
    public class OrderStatusChangeResponseDto : IDto
    {
        public int OrderNumber { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}

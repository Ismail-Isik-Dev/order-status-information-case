using Ordering.Entities;

namespace Ordering.Dtos
{
    public class OrderChangeStatusDto
    {
        public int CustomerId { get; set; }
        public int OrderNumber { get; set; }
        public OrderStatus Status { get; set; }
    }
}

using Ordering.Comman;

namespace Ordering.Dtos
{
    public class OrderCreateResponseDto : IDto
    {
        public int OrderNumber { get; set; }
        public Guid SystemOrderNumber { get; set; }
        public OrderCreateStatus Status { get; set; }
        public string Message { get; set; }
    }

    public enum OrderCreateStatus
    {
        Success = 1, 
        Failed = 2
    }
}

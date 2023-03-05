using Ordering.Comman;
using Ordering.Entities;

namespace Ordering.Dtos
{
    public class OrderCreateDto : IDto
    {
        public int CustomerId { get; set; }
        public int OrderNumber { get; set; }
        public string DestinationAddress { get; set; }
        public int Quantity { get; set; }
        public OrderQuantityUnit QuantityUnit { get; set; }
        public decimal Weight { get; set; }
        public OrderWeightUnit WeightUnit { get; set; }
        public string MaterialName { get; set; }
        public int MaterialCode { get; set; }
        public string Notes { get; set; }
    }
}

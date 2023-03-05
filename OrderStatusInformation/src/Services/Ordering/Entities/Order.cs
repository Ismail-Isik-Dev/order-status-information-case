using Ordering.Comman;

namespace Ordering.Entities
{
    public class Order : BaseDomainEntity, IEntity
    {
        public int CustomerId { get; set; }
        public int OrderNumber { get; set; }
        public OrderStatus Status { get; set; }
        public string DestinationAddress { get; set; }
        public int Quantity { get; set; }
        public OrderQuantityUnit QuantityUnit { get; set; }
        public decimal Weight { get; set; }
        public OrderWeightUnit WeightUnit { get; set; }
        public string MaterialName { get; set; }
        public int MaterialCode { get; set; }
        public string Notes { get; set; }
        public Guid SystemOrderNumber { get; set; }
    }

    public enum OrderQuantityUnit
    {
        Piece = 1,
        Parcel = 2,
        Package = 3,
        Palette = 4,
    }

    public enum OrderWeightUnit 
    {
        Kg = 1,
        Ton = 2,
    }

    public enum OrderStatus
    {
        OrderTaken = 1,
        SetOff = 2,
        AtDistributionCenter = 3,
        Distributed = 4,
        Delivered = 5,
        Undeliverable = 6,
    }
}

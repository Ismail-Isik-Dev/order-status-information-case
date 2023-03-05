using Ordering.Dtos;
using Ordering.Entities;

namespace Ordering.Dtos.Mappings
{
    public static class OrderMappings
    {
        public static List<Order> ToOrderList(this List<OrderCreateDto> ordersListDto)
        {
            var orderList = new List<Order>();

            foreach (var orderDto in ordersListDto)
            {
                orderList.Add(orderDto.ToOrder());
            }

            return orderList;
        }

        public static Order ToOrder(this OrderCreateDto order)
        {
            return new Order
            {
                OrderNumber = order.OrderNumber,
                MaterialCode = order.MaterialCode,
                MaterialName = order.MaterialName,
                DestinationAddress = order.DestinationAddress,
                Quantity = order.Quantity,
                QuantityUnit = order.QuantityUnit,
                Weight = order.Weight,
                WeightUnit = order.WeightUnit,
                Notes = order.Notes
            };
        }
    }
}

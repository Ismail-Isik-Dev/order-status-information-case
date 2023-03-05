using Ordering.Services.Contracts;
using Ordering.Dtos;
using Ordering.Entities;
using Ordering.Repositories.Contracts;

namespace Ordering.Services.Concretes
{
    public class OrderManager : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMaterialRepository _materialRepository;
        private readonly ILogger<OrderManager> _logger;
        private readonly RabbitMQProducer _rabbitmqProducer;

        public OrderManager(IOrderRepository orderRepository, IMaterialRepository materialRepository, ILogger<OrderManager> logger, RabbitMQProducer rabbitmqProducer)
        {
            _orderRepository = orderRepository;
            _materialRepository = materialRepository;
            _logger = logger;
            _rabbitmqProducer = rabbitmqProducer;
        }

        public async Task<OrderCreateResponseDto> CreateOrderAsync(Order order)
        {
            var checkRepetitiveCustomerOrderNumber = await _orderRepository.AnyAsync(x => x.CustomerId == order.CustomerId && x.OrderNumber == order.OrderNumber);

            if (checkRepetitiveCustomerOrderNumber)
            {
                // throw new Exception($"Orders belonging to the same customer repetitive order number cannot be received. CustomerId: {orderCreateDto.CustomerId}, OrderNumber: {orderCreateDto.OrderNumber}");

                _logger.LogError("Orders belonging to the same customer repetitive order number cannot be received.",
                    new { CustomerId = order.CustomerId, OrderNumber = order.OrderNumber });

                return new OrderCreateResponseDto
                {
                   OrderNumber = order.OrderNumber,
                   Status = OrderCreateStatus.Failed,
                   Message = $"Orders belonging to the same customer repetitive order number cannot be received. CustomerId: {order.CustomerId}, OrderNumber: {order.OrderNumber}"
                };
            }

            var isMaterialExist = await _materialRepository.AnyAsync(x => x.Code == order.MaterialCode);

            if (!isMaterialExist)
            {
                await _materialRepository.CreateAsync(new Material
                { 
                    Name = order.MaterialName,
                    Code = order.MaterialCode
                });

                _logger.LogInformation("Added material that is not in the table",
                   new { name = order.MaterialName, code = order.MaterialCode });
            }

            var response = await _orderRepository.CreateAsync(order);

            if (response == null)
            {
                // throw new Exception($"Orders not created. CustomerId: {orderCreateDto.CustomerId}, OrderNumber: {orderCreateDto.OrderNumber}");

                _logger.LogError("Orders not created!",
                    new { CustomerId = order.CustomerId, OrderNumber = order.OrderNumber });

                return new OrderCreateResponseDto
                {
                    OrderNumber = order.OrderNumber,
                    Status = OrderCreateStatus.Failed,
                    Message = $"Orders not created. CustomerId: {order.CustomerId}, OrderNumber: {order.OrderNumber}"
                };
            }

            _logger.LogInformation($"Order create operation success",
                   new { customerId = order.CustomerId, orderNumber = order.OrderNumber });

            return new OrderCreateResponseDto
            {
                OrderNumber = response.OrderNumber,
                SystemOrderNumber = response.SystemOrderNumber,
                Status = OrderCreateStatus.Success
            };

            // Notes: According Restful Api concept should throw any type exeption, not return result. 
            // TODO: Exception handling will be written
        }

        public async Task<OrderStatusChangeResponseDto> ChangeOrderStatusAsync(OrderChangeStatusDto order)
        {
            var orderToChangeStatus = await _orderRepository.GetAsync(x => x.CustomerId == order.CustomerId && x.OrderNumber == order.OrderNumber);

            if (orderToChangeStatus == null)
            {
                _logger.LogError("Order not found!",
                    new { customerId = order.CustomerId, orderNumber = order.OrderNumber });

                throw new Exception($"Order not found! CustomerId: {order.CustomerId}, OrderNumber: {order.OrderNumber}");
            }

            try
            {
                orderToChangeStatus.Status = order.Status;

                await _orderRepository.ChangeOrderStatus(orderToChangeStatus);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Order status not changed! Message: {ex.Message}",
                    new { customerId = order.CustomerId, orderNumber = order.OrderNumber });

                throw new Exception($"Order status not changed! Message: {ex.Message}");
            }

            _logger.LogInformation($"Order status change operation success",
                    new { customerId = order.CustomerId, orderNumber = order.OrderNumber });

            var result = new OrderStatusChangeResponseDto
            {
                OrderNumber = order.OrderNumber,
                Status = order.Status,
                ModifiedDate = DateTime.Now,
            };

            //When the status of the order changes, this information is added to the message queue. Any service that consumes this message queue can notify the relevant customer in any way.(mail notification, mobile notification, web notification etc.).

            _rabbitmqProducer.Publish(result);


            return result;
        }
    }
}

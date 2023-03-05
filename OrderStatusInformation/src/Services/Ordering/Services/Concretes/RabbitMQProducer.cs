using System.Text.Json;
using System.Text;
using Ordering.Dtos;
using RabbitMQ.Client;

namespace Ordering.Services.Concretes
{
    public class RabbitMQProducer
    {
        private readonly RabbitMQClientService _rabbitMQClientService;

        public RabbitMQProducer(RabbitMQClientService rabbitMQClientService)
        {
            _rabbitMQClientService = rabbitMQClientService;
        }

        public void Publish(OrderStatusChangeResponseDto changeData)
        {
            var channel = _rabbitMQClientService.Connect();

            var bodyString = JsonSerializer.Serialize(changeData);

            var bodyByte = Encoding.UTF8.GetBytes(bodyString);

            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            channel.BasicPublish(exchange: RabbitMQClientService.ExchangeName, routingKey: RabbitMQClientService.RoutingKey, basicProperties: properties, body: bodyByte);

        }
    }
}

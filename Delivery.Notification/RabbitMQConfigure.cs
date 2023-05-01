using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Delivery.Notification;

/// <summary>
/// RabbitMQ configuration
/// </summary>
public class RabbitMqConfigure {
    /// <summary>
    /// RabbitMQ configuration
    /// </summary>
    public static void Configure() {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        channel.ExchangeDeclare(exchange: "notifications", type: ExchangeType.Fanout);

        var queueName = channel.QueueDeclare().QueueName;
        channel.QueueBind(queue: queueName,
            exchange: "notifications",
            routingKey: string.Empty);

        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += (sender, e) => {
            var body = e.Body;
            var message = Encoding.UTF8.GetString(body.ToArray());
        };

        channel.BasicConsume(queue: queueName,
            autoAck: true,
            consumer: consumer);
    }
}
using System.Text;
using System.Text.Json;
using Delivery.Common.DTO;
using Delivery.Common.Interfaces;
using RabbitMQ.Client;

namespace Delivery.BackendAPI.BL.Services; 

/// <summary>
/// Notification queue service
/// </summary>
public class NotificationQueueService : INotificationQueueService {
    /// <summary>
    /// Sends notification to user
    /// </summary>
    /// <param name="messageDto"></param>
    public Task SendNotificationAsync(MessageDto messageDto) {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        try {
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel()) {
                channel.ExchangeDeclare(exchange: "notifications", type: ExchangeType.Fanout);
                
                string message = JsonSerializer.Serialize(messageDto);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "notifications",
                    routingKey: "",
                    basicProperties: null,
                    body: body);
            }
        }
        catch (Exception e) {
            // ignored
        }
        return Task.CompletedTask;
    }
}
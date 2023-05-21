using System.Text;
using System.Text.Json;
using Delivery.Common.DTO;
using Delivery.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace Delivery.BackendAPI.BL.Services; 

/// <summary>
/// Notification queue service
/// </summary>
public class NotificationQueueService : INotificationQueueService {
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="configuration"></param>
    public NotificationQueueService(IConfiguration configuration) {
        _configuration = configuration;
    }

    /// <summary>
    /// Sends notification to user
    /// </summary>
    /// <param name="messageDto"></param>
    public Task SendNotificationAsync(MessageDto messageDto) {
        var factory = new ConnectionFactory() { HostName = _configuration["RabbitMQ:HostName"] };
        try {
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.ExchangeDeclare(exchange: _configuration["RabbitMQ:ExchangeName"], type: ExchangeType.Fanout);
                
            var message = JsonSerializer.Serialize(messageDto);
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: _configuration["RabbitMQ:ExchangeName"],
                routingKey: "",
                basicProperties: null,
                body: body);
        }
        catch (Exception) {
            // ignored
        }
        return Task.CompletedTask;
    }
}
using System.Text;
using System.Text.Json;
using Delivery.Common.DTO;
using Delivery.Notification.DAL;
using Delivery.Notification.DAL.Entities;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Delivery.Notification.BL.Services;

/// <summary>
/// RabbitMQ service for receiving notifications from other services
/// </summary>
public class RabbitMqService : BackgroundService {
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly IConfiguration _configuration;
    private readonly ILogger<RabbitMqService> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="configuration"></param>
    /// <param name="logger"></param>
    /// <param name="serviceScopeFactory"></param>
    public RabbitMqService(IConfiguration configuration, ILogger<RabbitMqService> logger, IServiceScopeFactory serviceScopeFactory) {
        _configuration = configuration;
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;

        var factory = new ConnectionFactory() { HostName = _configuration["RabbitMQ:HostName"] };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.ExchangeDeclare(exchange: "notifications", type: ExchangeType.Fanout);

        var queueName = _channel.QueueDeclare(queue: _configuration["RabbitMQ:QueueName"]).QueueName;
        _channel.QueueBind(queue: queueName,
            exchange: "notifications",
            routingKey: string.Empty);
    }

    /// <summary>
    /// Executes service
    /// </summary>
    /// <param name="stoppingToken"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    protected override Task ExecuteAsync(CancellationToken stoppingToken) {
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += async (model, ea) => {
            try {
                var rawMessage = Encoding.UTF8.GetString(ea.Body.ToArray());
                var message = JsonSerializer.Deserialize<MessageDto>(rawMessage);
                if (message == null) throw new InvalidOperationException();

                using (var scope = _serviceScopeFactory.CreateScope()) {
                    var dbContext = scope.ServiceProvider.GetRequiredService<NotificationDbContext>();
                    await dbContext.Messages.AddAsync(new Message() {
                        Id = Guid.NewGuid(),
                        ReceiverId = message.ReceiverId,
                        Text = message.Text,
                        Title = message.Title,
                        CreatedAt = DateTime.UtcNow
                    });
                
                    await dbContext.SaveChangesAsync(stoppingToken);
                }
                _logger.LogInformation($"Received message");
            }
            catch (Exception ex) {
                _logger.LogError(ex, "Error processing message");
            }
        };

        _channel.BasicConsume(queue: _configuration["RabbitMQ:QueueName"], autoAck: true, consumer: consumer);

        return Task.CompletedTask;
    }

    /// <summary>
    /// Disposes service
    /// </summary>
    public override void Dispose() {
        _channel.Close();
        _connection.Close();
        base.Dispose();
    }
}
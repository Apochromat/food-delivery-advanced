using System.Text;
using System.Text.Json;
using Delivery.Common.DTO;
using Delivery.Notification.DAL;
using Delivery.Notification.DAL.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Delivery.Notification.BL.Services;

public class RabbitMqService : BackgroundService {
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly IConfiguration _configuration;
    private readonly ILogger<RabbitMqService> _logger;
    private readonly NotificationDbContext _dbContext;

    public RabbitMqService(IConfiguration configuration, ILogger<RabbitMqService> logger, NotificationDbContext dbContext) {
        _configuration = configuration;
        _logger = logger;
        _dbContext = dbContext;

        var factory = new ConnectionFactory() { HostName = _configuration["RabbitMQ:HostName"] };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.ExchangeDeclare(exchange: "notifications", type: ExchangeType.Fanout);

        var queueName = _channel.QueueDeclare(queue: _configuration["RabbitMQ:QueueName"]).QueueName;
        _channel.QueueBind(queue: queueName,
            exchange: "notifications",
            routingKey: string.Empty);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken) {
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (model, ea) => {
            try {
                var rawMessage = Encoding.UTF8.GetString(ea.Body.ToArray());
                var message = JsonSerializer.Deserialize<MessageDto>(rawMessage);
                if (message == null) throw new InvalidOperationException();

                // TODO: добавить логику записи сообщения в базу данных и отправки через SignalR
                
                _dbContext.Messages.Add(new Message() {
                    Id = Guid.NewGuid(),
                    ReceiverId = message.ReceiverId,
                    Text = message.Text,
                    Title = message.Title,
                    CreatedAt = DateTime.UtcNow
                });
                
                _dbContext.SaveChangesAsync(stoppingToken);
                
                _logger.LogInformation($"Received message: {message}");
            }
            catch (Exception ex) {
                _logger.LogError(ex, "Error processing message");
            }
        };

        _channel.BasicConsume(queue: _configuration["RabbitMQ:QueueName"], autoAck: true, consumer: consumer);

        return Task.CompletedTask;
    }

    public override void Dispose() {
        _channel.Close();
        _connection.Close();
        base.Dispose();
    }
}
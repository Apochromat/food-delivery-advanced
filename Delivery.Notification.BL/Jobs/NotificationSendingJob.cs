using Delivery.Common.DTO;
using Delivery.Common.Interfaces;
using Delivery.Notification.DAL;
using Microsoft.Extensions.Logging;
using Quartz;

namespace Delivery.Notification.BL.Jobs; 

/// <summary>
/// Job for sending notifications
/// </summary>
public class NotificationSendingJob : IJob {
    private readonly ILogger<NotificationSendingJob> _logger;
    private readonly NotificationDbContext _dbContext;
    private readonly INotificationService _notificationService;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="dbContext"></param>
    /// <param name="notificationService"></param>
    public NotificationSendingJob(ILogger<NotificationSendingJob> logger, NotificationDbContext dbContext, INotificationService notificationService) {
        _dbContext = dbContext;
        _notificationService = notificationService;
        _logger = logger;
    }

    /// <summary>
    /// Executes job
    /// </summary>
    /// <param name="context"></param>
    public async Task Execute(IJobExecutionContext context) {
        _logger.LogInformation("NotificationSendingJob is running");
        var notifications = _dbContext.Messages.Where(m => m.DeliveredAt == null).ToList();
        foreach (var notification in notifications) {
            var sent = await _notificationService.SendNotificationAsync(new MessageDto() {
                ReceiverId = notification.ReceiverId,
                Text = notification.Text,
                Title = notification.Title,
                CreatedAt = notification.CreatedAt
            });
            
            if (sent) {
                notification.DeliveredAt = DateTime.UtcNow;
            }
        }
        _dbContext.SaveChanges();
    }
}
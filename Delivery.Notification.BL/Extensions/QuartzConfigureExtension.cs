using Delivery.Notification.BL.Jobs;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace Delivery.Notification.BL.Extensions;

public static class QuartzConfigureExtension {
    public static IServiceCollection ConfigureQuartz(this IServiceCollection services) {
        services.AddQuartz(q => {
            q.UseMicrosoftDependencyInjectionScopedJobFactory();
            // Just use the name of your job that you created in the Jobs folder.
            var jobKey = new JobKey("NotificationSendingJob");
            q.AddJob<NotificationSendingJob>(opts => opts.WithIdentity(jobKey));

            q.AddTrigger(opts => opts
                .ForJob(jobKey)
                .WithIdentity("NotificationSendingJob-trigger")
                //This Cron interval can be described as "run every minute" (when second is zero)
                .WithCronSchedule("0 * * ? * *")
            );
        });
        services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
        return services;
    }
}
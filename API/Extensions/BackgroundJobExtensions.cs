using Application.Persistence;
using Hangfire;

namespace API.Extensions
{
    public static class BackgroundJobExtensions
    {
        public static IApplicationBuilder UseBackgroundJobs(this WebApplication app)
        {
            app.Services
                .GetRequiredService<IRecurringJobManager>()
                .AddOrUpdate<INotificationService>(
                    "delete-notifications",
                    job => job.DeleteNotificationsOlderThanADay(),
                    "0/15 * * * * *"
                );
            return app;
        }
    }
}

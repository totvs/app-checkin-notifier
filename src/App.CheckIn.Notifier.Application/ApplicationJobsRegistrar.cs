using System.Threading;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.Common;
using Microsoft.Extensions.Hosting;

namespace AppCheckInNotifier
{
    /// <summary>
    /// Background Service used to register jobs
    /// </summary>
    public class ApplicationJobsRegistrar : BackgroundService
    {
        private readonly IRecurringJobManager _recurringJobManager;

        public ApplicationJobsRegistrar(IRecurringJobManager recurringJobManager)
        {
            _recurringJobManager = recurringJobManager;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //Removing old job
            _recurringJobManager.RemoveIfExists("TalkAt15MinutesNotifier");
            
            _recurringJobManager.AddOrUpdate(
                "EventAt15MinutesNotifier",
                Job.FromExpression<EventAt15MinutesNotifier>(notifier => notifier.NotifyAsync()),
                Cron.Minutely());

            return Task.CompletedTask;
        }
    }
}

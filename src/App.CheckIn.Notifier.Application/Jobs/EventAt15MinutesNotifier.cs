using System;
using System.Globalization;
using System.Threading.Tasks;
using App.CheckIn.Domain.Repositories;
using App.CheckIn.Domain.ValuesObjects;
using AppCheckInNotifier.Application.Services;
using Microsoft.Extensions.Logging;

namespace AppCheckInNotifier
{
    /// <summary>
    /// Defines a job that will send notification to event subscriptions starting between now and 15 minutes.
    /// </summary>
    public class EventAt15MinutesNotifier
    {
        private readonly IEventSubscriptionRepository _eventSubscriptionRepository;
        private readonly INotificationService _notificationService;
        private readonly ILogger<EventAt15MinutesNotifier> _logger;

        public EventAt15MinutesNotifier(
            IEventSubscriptionRepository eventSubscriptionRepository,
            INotificationService notificationService,
            ILogger<EventAt15MinutesNotifier> logger)
        {
            _eventSubscriptionRepository = eventSubscriptionRepository;
            _notificationService = notificationService;
            _logger = logger;
        }

        public async Task NotifyAsync()
        {
            CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("pt-BR");

            var range = new DateRange(DateTimeOffset.Now, DateTimeOffset.Now.AddMinutes(15));

            var subscriptions = await _eventSubscriptionRepository.FindEventsStartingBetweenAsync(range);

            if (subscriptions.Count > 0)
            {
                await _notificationService.NotifyAttendantsAsync(subscriptions);

                subscriptions.ForEach(s => s.MarkAsNotified());

                await _eventSubscriptionRepository.UpdateSubscriptionsAsync(subscriptions);
            }
            else
            {
                _logger.NoSubscriptionsPendingNotification();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.CheckIn.Domain;
using AppCheckInNotifier.Application.FirebaseCloudMessaging;
using AppCheckInNotifier.Application.Localization;
using FCM.Net;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Tnf.Localization;

namespace AppCheckInNotifier.Application.Services
{
    /// <summary>
    /// Service for send event notification to Firebase
    /// </summary>
    public class FirebaseNotificationService : INotificationService
    {
        private readonly FirebaseCloudMessagingOptions _options;
        private readonly ILocalizationSource _applicationLocalizationSource;
        private readonly ILogger<FirebaseNotificationService> _logger;

        public FirebaseNotificationService(
            IOptions<FirebaseCloudMessagingOptions> options,
            ILocalizationManager localizationManager,
            ILogger<FirebaseNotificationService> logger)
        {
            _options = options.Value;
            _applicationLocalizationSource = localizationManager.GetSource(LocalizationSources.Application);
            _logger = logger;
        }

        public async Task NotifyAttendantsAsync(List<EventSubscription> subscriptions)
        {
            var now = DateTimeOffset.Now;
            var titleFormat = _applicationLocalizationSource.GetString(NotificationStrings.NoticationTileFormat);
            var bodyFormat = _applicationLocalizationSource.GetString(NotificationStrings.NoticationBodyFormat);

            _logger.LogNotificationsCount(subscriptions.Count);
            _logger.SendingNotificationsToServerKey(_options.ServerKey);

            using (var sender = new Sender(_options.ServerKey))
            {
                foreach (var subscription in subscriptions)
                {
                    var message = new Message
                    {
                        RegistrationIds = new List<string> { subscription.NotificationToken },
                        Notification = CreateNotification(subscription, now, titleFormat, bodyFormat)
                    };

                    _logger.LogNotification(message);

                    var response = await sender.SendAsync(message);

                    _logger.LogResponseContent(response);
                }
            }
        }

        private static Notification CreateNotification(
            EventSubscription subscription,
            DateTimeOffset now,
            string titleFormat,
            string bodyFormat)
        {
            var timeToEventStart = subscription.EventStartTime - now;

            return new Notification
            {
                Title = string.Format(titleFormat, subscription.EventName, (int)timeToEventStart.TotalMinutes),
                Body = string.Format(bodyFormat, subscription.EventName, (int)timeToEventStart.TotalMinutes, subscription.EventRoom)
            };
        }

        enum NotificationStrings
        {
            NoticationTileFormat,
            NoticationBodyFormat
        }
    }
}

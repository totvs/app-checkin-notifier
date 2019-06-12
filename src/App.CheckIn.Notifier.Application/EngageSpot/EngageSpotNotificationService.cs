using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using App.CheckIn.Domain;
using AppCheckInNotifier.Application.Localization;
using AppCheckInNotifier.Application.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Tnf.Localization;

namespace App.CheckIn.Notifier.Application.EngageSpot
{
    public class EngageSpotNotificationService : INotificationService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILocalizationSource _applicationLocalizationSource;
        private readonly ILogger<EngageSpotNotificationService> _logger;
        private readonly EngageSpotOptions _options;

        public EngageSpotNotificationService(
            IHttpClientFactory httpClientFactory,
            ILocalizationManager localizationManager,
            ILogger<EngageSpotNotificationService> logger,
            IOptions<EngageSpotOptions> options)
        {
            _httpClientFactory = httpClientFactory;
            _applicationLocalizationSource = localizationManager.GetSource(LocalizationSources.Application);
            _logger = logger;
            _options = options.Value;
        }

        public async Task NotifyAttendantsAsync(List<EventSubscription> subscriptions)
        {
            subscriptions = subscriptions
                .Where(s => s.NotificationService == NotificationServiceType.EngageSpot)
                .ToList();

            if (subscriptions.Count <= 0)
            {
                return;
            }

            var now = DateTimeOffset.Now;
            var titleFormat = _applicationLocalizationSource.GetString(NotificationStrings.NoticationTileFormat);
            var bodyFormat = _applicationLocalizationSource.GetString(NotificationStrings.NoticationBodyFormat);

            _logger.LogNotificationsCount(subscriptions.Count);
            _logger.LogEngageSpotApiKey(_options.ApiKey);

            var httpClient = _httpClientFactory.CreateClient("EngageSpot");

            foreach (var subscription in subscriptions)
            {
                var timeToEventStart = subscription.EventStartTime - now;
                var title = string.Format(titleFormat, subscription.EventName, (int)timeToEventStart.TotalMinutes);
                var textMessage = string.Format(bodyFormat, subscription.EventName, (int)timeToEventStart.TotalMinutes, subscription.EventRoom);

                var message = EngageSpotMessage.BuildDefaultMessage(subscription.NotificationToken, title, textMessage);

                _logger.LogEngageSpotMessage(message);

                var response = await httpClient.PostAsync("campaigns", new StringContent(JsonConvert.SerializeObject(message)));

                _logger.LogEngageSpotResponse(await response.Content.ReadAsStringAsync());
            }
        }
    }
}

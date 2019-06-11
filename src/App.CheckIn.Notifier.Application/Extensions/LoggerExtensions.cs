using System;
using FCM.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Microsoft.Extensions.Logging
{
    public static class LoggerExtensions
    {
        private static readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = NullValueHandling.Ignore,
            DateFormatHandling = DateFormatHandling.IsoDateFormat,
            Formatting = Formatting.Indented
        };

        private static readonly Action<ILogger, string, Exception> _sendingNotificationsToServerKey =
            LoggerMessage.Define<string>(LogLevel.Information, 1, "Sending notifications with server key {serverKey}");

        private static readonly Action<ILogger, int, Exception> _logNotificationsCount =
            LoggerMessage.Define<int>(LogLevel.Information, 1, "Sending {count} notifications");

        private static readonly Action<ILogger, string, Exception> _logNotification =
            LoggerMessage.Define<string>(LogLevel.Information, 1, "Sending message" + Environment.NewLine + "{@message}");

        private static readonly Action<ILogger, string, Exception> _logResponseContent =
            LoggerMessage.Define<string>(LogLevel.Information, 1, "Response" + Environment.NewLine + "{@responseContent}");

        private static readonly Action<ILogger, Exception> _noSubscriptionsPendingNotification =
            LoggerMessage.Define(LogLevel.Information, 1, "No subscriptions found needing notifications");

        public static void SendingNotificationsToServerKey(this ILogger logger, string serverKey)
        {
            _sendingNotificationsToServerKey(logger, serverKey, null);
        }

        public static void LogNotificationsCount(this ILogger logger, int notificationCount)
        {
            _logNotificationsCount(logger, notificationCount, null);
        }

        public static void LogNotification(this ILogger logger, Message message)
        {
            _logNotification(logger, JsonConvert.SerializeObject(message, _serializerSettings), null);
        }

        public static void LogResponseContent(this ILogger logger, ResponseContent responseContent)
        {
            _logResponseContent(logger, JsonConvert.SerializeObject(responseContent, _serializerSettings), null);
        }

        public static void NoSubscriptionsPendingNotification(this ILogger logger)
        {
            _noSubscriptionsPendingNotification(logger, null);
        }
    }
}

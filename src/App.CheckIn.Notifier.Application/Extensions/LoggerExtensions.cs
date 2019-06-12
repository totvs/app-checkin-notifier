using System;
using App.CheckIn.Notifier.Application.EngageSpot;
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

        private static readonly Action<ILogger, string, Exception> _logFMCServerKey =
            LoggerMessage.Define<string>(LogLevel.Information, 1, "Sending notifications with server key {serverKey}");

        private static readonly Action<ILogger, int, Exception> _logNotificationsCount =
            LoggerMessage.Define<int>(LogLevel.Information, 1, "Sending {count} notifications");

        private static readonly Action<ILogger, string, Exception> _logFMCMessage =
            LoggerMessage.Define<string>(LogLevel.Information, 1, "Sending message" + Environment.NewLine + "{@message}");

        private static readonly Action<ILogger, string, Exception> _logFMCResponse =
            LoggerMessage.Define<string>(LogLevel.Information, 1, "Response" + Environment.NewLine + "{@responseContent}");

        private static readonly Action<ILogger, Exception> _noSubscriptionsPendingNotification =
            LoggerMessage.Define(LogLevel.Information, 1, "No subscriptions found needing notifications");

        private static readonly Action<ILogger, string, Exception> _logEngageSpotApiKey =
            LoggerMessage.Define<string>(LogLevel.Information, 1, "Sending notifications with Api-Key {ApiKey}");

        private static readonly Action<ILogger, string, Exception> _logEngageSpotMessage =
            LoggerMessage.Define<string>(LogLevel.Information, 1, "Sending message" + Environment.NewLine + "{@message}");

        private static readonly Action<ILogger, string, Exception> _logEngageSpotResponse =
            LoggerMessage.Define<string>(LogLevel.Information, 1, "Response" + Environment.NewLine + "{@responseContent}");

        public static void LogFMCServerKey(this ILogger logger, string serverKey)
        {
            _logFMCServerKey(logger, serverKey, null);
        }

        public static void LogNotificationsCount(this ILogger logger, int notificationCount)
        {
            _logNotificationsCount(logger, notificationCount, null);
        }

        public static void LogFMCMessage(this ILogger logger, Message message)
        {
            _logFMCMessage(logger, JsonConvert.SerializeObject(message, _serializerSettings), null);
        }

        public static void LogFMCResponse(this ILogger logger, ResponseContent responseContent)
        {
            _logFMCResponse(logger, JsonConvert.SerializeObject(responseContent, _serializerSettings), null);
        }

        public static void NoSubscriptionsPendingNotification(this ILogger logger)
        {
            _noSubscriptionsPendingNotification(logger, null);
        }

        public static void LogEngageSpotApiKey(this ILogger logger, string apiKey)
        {
            _logEngageSpotApiKey(logger, apiKey, null);
        }

        public static void LogEngageSpotMessage(this ILogger logger, EngageSpotMessage message)
        {
            _logEngageSpotMessage(logger, JsonConvert.SerializeObject(message, _serializerSettings), null);
        }

        public static void LogEngageSpotResponse(this ILogger logger, string responseContent)
        {
            _logEngageSpotResponse(logger, responseContent, null);
        }


    }
}

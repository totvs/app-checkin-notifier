using System;

namespace App.CheckIn.Domain
{
    /// <summary>
    /// Entity representing an user subscription to an event
    /// </summary>
    public class EventSubscription
    {
        public EventSubscription(
            long userId,
            string userEmail,
            string eventCode,
            DateTimeOffset eventStartTime,
            TimeSpan eventDuration,
            string eventName,
            string eventDescription,
            string eventRoom,
            bool enablePushNotification,
            string notificationToken)
        {
            Id = Guid.NewGuid();

            UserId = userId;
            UserEmail = userEmail;
            EventCode = eventCode;
            EventStartTime = eventStartTime;
            EventDuration = eventDuration;
            EventName = eventName;
            EventDescription = eventDescription;
            EventRoom = eventRoom;
            EnablePushNotification = enablePushNotification;
            NotificationToken = notificationToken;
        }

        /// <summary>
        /// Unique identifier of the subscription
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Identifier of the user
        /// </summary>
        public long UserId { get; private set; }

        /// <summary>
        /// Email of the user
        /// </summary>
        public string UserEmail { get; private set; }

        /// <summary>
        /// Code identifier of the Event
        /// </summary>
        public string EventCode { get; private set; }

        /// <summary>
        /// Name of the event
        /// </summary>
        public string EventName { get; private set; }

        /// <summary>
        /// Description of the event
        /// </summary>
        public string EventDescription { get; private set; }

        /// <summary>
        /// Room of the event
        /// </summary>
        public string EventRoom { get; private set; }

        /// <summary>
        /// Start time of the event
        /// </summary>
        public DateTimeOffset EventStartTime { get; private set; }

        /// <summary>
        /// Duration of the event
        /// </summary>
        public TimeSpan EventDuration { get; private set; }

        /// <summary>
        /// Indicates whether or not the user wants to receive notifications of this subscription
        /// </summary>
        public bool EnablePushNotification { get; private set; }

        /// <summary>
        /// Notification token required to send notification do the user device
        /// </summary>
        public string NotificationToken { get; private set; }

        /// <summary>
        ///  Notification was already send to the user
        /// </summary>
        public bool Notified { get; private set; }

        /// <summary>
        /// Mark this subscription as notified
        /// </summary>
        public void MarkAsNotified() => Notified = true;
    }
}

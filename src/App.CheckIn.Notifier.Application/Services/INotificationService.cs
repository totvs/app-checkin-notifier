using System.Collections.Generic;
using System.Threading.Tasks;
using App.CheckIn.Domain;

namespace AppCheckInNotifier.Application.Services
{
    /// <summary>
    /// Defines a service to notify attendants of event subscriptions
    /// </summary>
    public interface INotificationService
    {
        Task NotifyAttendantsAsync(List<EventSubscription> subscriptions);
    }
}

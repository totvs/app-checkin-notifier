using System.Collections.Generic;
using System.Threading.Tasks;
using App.CheckIn.Domain.ValuesObjects;
using Tnf.Dto;

namespace App.CheckIn.Domain.Repositories
{
    /// <summary>
    /// Defines methods to interact with event subscriptions in storage
    /// </summary>
    public interface IEventSubscriptionRepository
    {
        /// <summary>
        /// Queries event subscriptions starting at a giving date range
        /// </summary>
        /// <param name="range">The data range </param>
        /// <returns>A list of event subscriptions starting at the giving range</returns>
        Task<List<EventSubscription>> FindEventsStartingBetweenAsync(DateRange range);

        /// <summary>
        /// Insert an <see cref="EventSubscription"/> into storage
        /// </summary>
        /// <param name="eventSubscription"></param>
        /// <returns>The <see cref="EventSubscription"/></returns>
        Task<EventSubscription> InsertAsync(EventSubscription eventSubscription);

        /// <summary>
        /// Attempts to retrieve an <see cref="EventSubscription"/> from storage
        /// </summary>
        /// <param name="userId">The identifier of the user owner of the subscription</param>
        /// <param name="eventCode">The event code of the subscription</param>
        /// <returns>The <see cref="EventSubscription"/> if found, otherwise returns null</returns>
        Task<EventSubscription> GetSubscriptionAsync(long userId, string eventCode);

        /// <summary>
        /// Updates a list of <see cref="EventSubscription"/>
        /// </summary>
        /// <param name="subscriptions">The list of <see cref="EventSubscription"/> to be updated</param>
        /// <returns>Task that will complete where the update finishes</returns>
        Task UpdateSubscriptionsAsync(List<EventSubscription> subscriptions);

        /// <summary>
        /// Get the event subscriptions for a giving user
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <param name="requestAllDto">The <see cref="RequestAllDto"/> containing information about paging</param>
        /// <returns>A <see cref="IListDto{EventSubscription}"/> containing the user event subscription</returns>
        Task<IListDto<EventSubscription>> GetUserSubscription(long userId, RequestAllDto requestAllDto);
    }
}

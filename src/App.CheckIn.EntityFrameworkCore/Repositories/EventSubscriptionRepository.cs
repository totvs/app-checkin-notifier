using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.CheckIn.Domain;
using App.CheckIn.Domain.Repositories;
using App.CheckIn.Domain.ValuesObjects;
using Microsoft.EntityFrameworkCore;
using Tnf.Dto;
using Tnf.EntityFrameworkCore;
using Tnf.EntityFrameworkCore.Repositories;

namespace App.CheckIn.EntityFrameworkCore.Repositories
{
    public class EventSubscriptionRepository : EfCoreRepositoryBase<AppCheckInDbContext, EventSubscription>, IEventSubscriptionRepository
    {
        public EventSubscriptionRepository(IDbContextProvider<AppCheckInDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public Task<List<EventSubscription>> FindEventsStartingBetweenAsync(DateRange range)
        {
            return Table
                .Where(subscription => subscription.EventStartTime >= range.Start
                                    && subscription.EventStartTime <= range.End
                                    && !subscription.Notified
                                    && subscription.EnablePushNotification)
               .ToListAsync();
        }

        public override async Task<EventSubscription> InsertAsync(EventSubscription entity)
        {
            Table.Add(entity);

            await Context.SaveChangesAsync();

            return entity;
        }

        public Task<EventSubscription> GetSubscriptionAsync(long userId, string eventCode)
        {
            return FirstOrDefaultAsync(es => es.UserId == userId && es.EventCode == eventCode);
        }

        public Task UpdateSubscriptionsAsync(List<EventSubscription> subscriptions)
        {
            foreach (var subscription in subscriptions)
            {
                Context.Entry(subscription).State = EntityState.Modified;
            }

            return Context.SaveChangesAsync();
        }

        public Task<IListDto<EventSubscription>> GetUserSubscription(long userId, RequestAllDto requestAllDto)
        {
            return Table.Where(es => es.UserId == userId)
                .ToListDtoAsync(requestAllDto);
        }

        public async Task<bool> HasSubscriptionAsync(long userId, string eventCode)
        {
            return await CountAsync(s => s.UserId == userId && s.EventCode == eventCode) > 0;
        }
    }
}

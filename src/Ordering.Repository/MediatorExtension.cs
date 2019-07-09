using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Ordering.Domain.Core;

namespace Ordering.Repository
{
    static class MediatorExtension
    {
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, IReadOnlyCollection<Entity> session)
        {
            var domainEvents = session
                .SelectMany(x => x.DomainEvents)
                .ToList();

            session.ToList()
                .ForEach(entity => entity.ClearDomainEvents());

            var tasks = domainEvents.Select(async (domainEvent) => {
                    await mediator.Publish(domainEvent);
                });

            await Task.WhenAll(tasks);
        }
    }
}

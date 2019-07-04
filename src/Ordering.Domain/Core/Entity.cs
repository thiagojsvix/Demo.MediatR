using System.Collections.Generic;
using MediatR;

namespace Ordering.Domain.Core
{
    public abstract class Entity: Validation
    {
        public long Id { get; protected set; }

        private List<INotification> _domainEvents;
        public IReadOnlyCollection<INotification> DomainEvents => this._domainEvents?.AsReadOnly();

        public void AddDomainEvent(INotification eventItem)
        {
            this._domainEvents = this._domainEvents ?? new List<INotification>();
            this._domainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(INotification eventItem)
        {
            this._domainEvents?.Remove(eventItem);
        }
    }
}

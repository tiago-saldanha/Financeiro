using FinanceManager.Domain.Interfaces;

namespace FinanceManager.Domain.Abstractions
{
    public abstract class Entity
    {
        private readonly List<IDomainEvent> _domainEvents = [];
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents;

        protected void AddDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);
        protected void ClearDomainEvents() => _domainEvents.Clear();
    }
}

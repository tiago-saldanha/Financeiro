using FinanceManager.Domain.Interfaces;

namespace FinanceManager.Domain.Abstractions
{
    public abstract class DomainEvent : IDomainEvent
    {
        public DateTime OcurredAt { get; } = DateTime.UtcNow;
    }
}

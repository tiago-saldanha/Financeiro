using FinanceManager.Domain.Abstractions;
using FinanceManager.Domain.Enums;

namespace FinanceManager.Domain.Events
{
    public class TransactionReopenEvent(Guid transactionId, TransactionStatus status) : DomainEvent
    {
        public Guid TransactionId { get; } = transactionId;
        public TransactionStatus Status { get; } = status;
    }
}

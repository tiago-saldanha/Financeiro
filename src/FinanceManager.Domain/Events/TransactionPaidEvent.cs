using FinanceManager.Domain.Abstractions;
using FinanceManager.Domain.Interfaces;

namespace FinanceManager.Domain.Events
{
    public class TransactionPaidEvent(Guid transactionId, DateTime paymentDate) : DomainEvent
    {
        public Guid TransactionId { get; } = transactionId;
        public DateTime PaymentDate { get; } = paymentDate;
    }
}

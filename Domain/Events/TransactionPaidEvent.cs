using Domain.Interfaces;

namespace Domain.Events
{
    public class TransactionPaidEvent(Guid transactionId, DateTime paymentDate) : IDomainEvent
    {
        public Guid TransactionId { get; } = transactionId;
        public DateTime PaymentDate { get; } = paymentDate;
        public DateTime OcurredAt { get; } = DateTime.UtcNow;
    }
}

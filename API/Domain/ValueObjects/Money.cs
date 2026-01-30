using API.Domain.Exceptions;

namespace API.Domain.ValueObjects
{
    public sealed record class Money
    {
        public decimal Value { get; }

        public Money(decimal value)
        {
            if (value < 0)
                throw new TransactionException("O valor não pode ser negativo");

            Value = value;
        }

        public static implicit operator decimal(Money money) 
            => money.Value;
    }
}

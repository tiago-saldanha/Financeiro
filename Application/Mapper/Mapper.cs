using Domain.Enums;
using Application.Exceptions;
using Application.Enums;

namespace Application.Mapper
{
    public static class Mapper
    {
        public static TransactionStatus TransactionStatus(TransactionStatusDto status) => status switch
        {
            TransactionStatusDto.pending => Domain.Enums.TransactionStatus.Pending,
            TransactionStatusDto.paid => Domain.Enums.TransactionStatus.Paid,
            TransactionStatusDto.cancelled => Domain.Enums.TransactionStatus.Cancelled,
            _ => throw new TransactionStatusAppException()
        };

        public static TransactionType TransactionType(TransactionTypeDto type) => type switch
        {
            TransactionTypeDto.revenue => Domain.Enums.TransactionType.Revenue,
            TransactionTypeDto.expense => Domain.Enums.TransactionType.Expense,
            _ => throw new TransactionTypeAppException()
        };

        public static TransactionType TransactionType(string transactionType) => transactionType.ToLower() switch
        {
            "revenue" => Domain.Enums.TransactionType.Revenue,
            "expense" => Domain.Enums.TransactionType.Expense,
            _ => throw new TransactionTypeAppException()
        };
    }
}

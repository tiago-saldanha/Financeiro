using API.Application.Enums;
using API.Application.Exceptions;
using API.Domain.Enums;

namespace API.Application.Mapper
{
    public static class Mapper
    {
        public static TransactionStatus MapTransactionStatus(TransactionStatusDto status) => status switch
        {
            TransactionStatusDto.Pending => TransactionStatus.Pending,
            TransactionStatusDto.Paid => TransactionStatus.Paid,
            TransactionStatusDto.Cancelled => TransactionStatus.Cancelled,
            _ => throw new TransactionStatusAppException()
        };

        public static TransactionType MapTransactionType(TransactionTypeDto type) => type switch
        {
            TransactionTypeDto.Revenue => TransactionType.Revenue,
            TransactionTypeDto.Expense => TransactionType.Expense,
            _ => throw new TransactionTypeAppException()
        };

        public static TransactionType MapTransactionType(string transactionType) => transactionType.ToLower() switch
        {
            "revenue" => TransactionType.Revenue,
            "expense" => TransactionType.Expense,
            _ => throw new TransactionTypeAppException()
        };
    }
}

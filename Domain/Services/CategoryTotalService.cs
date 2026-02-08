using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects;

namespace Domain.Services
{
    public class CategoryTotalService
    {
        public CategoryBalance Calculate(Category category)
        {
            var received = category.Transactions
                .Where(t => t.Type == TransactionType.Revenue)
                .Sum(t => t.Amount);

            var spent = category.Transactions
                .Where(t => t.Type == TransactionType.Expense)
                .Sum(t => t.Amount);

            var balance = received - spent;

            return new CategoryBalance(received, spent, balance);
        }
    }
}

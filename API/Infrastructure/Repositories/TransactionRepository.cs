using System.Linq.Expressions;
using API.Data;
using API.Domain.Entities;
using API.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace API.Infrastructure.Repositories
{
    public class TransactionRepository(AppDbContext context) : ITransactionRepository
    {
        public async Task<Transaction> CreateAsync(Transaction transaction)
        {
            await context.Transactions.AddAsync(transaction);
            await context.SaveChangesAsync();
            return transaction;
        }

        public Task<List<Transaction>> GetAllAsync()
        {
            return context.Transactions
                .Include(q => q.Category)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Transaction> GetByIdAsync(Guid id)
        {
            var transaction = await context.Transactions
                .Include(q => q.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(q => q.Id == id) ?? 
                throw new KeyNotFoundException("Transaction not found");

            return transaction;
        }

        public async Task<Transaction> UpdateAsync(Transaction transaction)
        {
            context.Transactions.Update(transaction);
            await context.SaveChangesAsync();
            return transaction;
        }

        public async Task<List<Transaction>> GetByFilterAsync(Expression<Func<Transaction, bool>> predicate)
        {
            var transactions = await context.Transactions.Where(predicate)
                .Include(q => q.Category)
                .AsNoTracking()
                .ToListAsync();

            return transactions;
        }
    }
}

using System.Linq.Expressions;
using API.Data;
using API.Domain.Entities;
using API.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace API.Infrastructure.Repositories
{
    public class TransactionRepository(AppDbContext context) : ITransactionRepository
    {
        public async Task CreateAsync(Transaction transaction)
            => await context.Transactions.AddAsync(transaction);

        public async Task<List<Transaction>> GetAllAsync()
            => await context.Transactions.Include(q => q.Category).AsNoTracking().ToListAsync();

        public async Task<Transaction> GetByIdAsync(Guid id)
            => await context.Transactions.Include(q => q.Category).AsNoTracking().FirstOrDefaultAsync(q => q.Id == id) ?? throw new KeyNotFoundInfraException("Transação não encontrada");

        public void Update(Transaction transaction)
            => context.Transactions.Update(transaction);

        public async Task<List<Transaction>> GetByFilterAsync(Expression<Func<Transaction, bool>> predicate)
            => await context.Transactions.Where(predicate).Include(q => q.Category).AsNoTracking().ToListAsync();
    }
}

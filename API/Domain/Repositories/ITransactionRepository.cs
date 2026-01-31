using System.Linq.Expressions;
using API.Domain.Entities;

namespace API.Domain.Repositories
{
    public interface ITransactionRepository
    {
        Task<Transaction> GetByIdAsync(Guid id);
        Task<Transaction> CreateAsync(Transaction request);
        Task<Transaction> UpdateAsync(Transaction request);
        Task<List<Transaction>> GetAllAsync();
        Task<List<Transaction>> GetByFilterAsync(Expression<Func<Transaction, bool>> predicate);
    }
}

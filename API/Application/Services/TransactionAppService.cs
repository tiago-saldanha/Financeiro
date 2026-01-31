using API.Application.DTOs.Requests;
using API.Application.DTOs.Responses;
using API.Application.Enums;
using API.Application.Exceptions;
using API.Application.Interfaces;
using API.Data;
using API.Domain.Entities;
using API.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace API.Application.Services
{
    public class TransactionAppService(AppDbContext context) : ITransactionAppService
    {
        public async Task<IEnumerable<TransactionResponse>> GetAllAsync()
        {
            var transactions = await context.Transactions
                .Where(q => q.Status != TransactionStatus.Cancelled)
                .Include(q => q.Category)
                .AsNoTracking()
                .ToListAsync();

            return transactions.Select(TransactionResponse.Create);
        }

        public async Task<IEnumerable<TransactionResponse>> GetByStatusAsync(TransactionStatusDto status)
        {
            var transactions = await context.Transactions
                .Where(q => q.Status == Map(status))
                .Include(q => q.Category)
                .AsNoTracking()
                .ToListAsync();

            return transactions.Select(TransactionResponse.Create);
        }

        public async Task<IEnumerable<TransactionResponse>> GetByTypeAsync(TransactionTypeDto type)
        {
            var transactions = await context.Transactions
                .Where(q => q.Type == Map(type))
                .Include(q => q.Category)
                .AsNoTracking()
                .ToListAsync();

            return transactions.Select(TransactionResponse.Create);
        }

        public async Task<TransactionResponse> CreateAsync(CreateTransactionRequest request)
        {
            var transaction = Transaction.Create(
                request.Description,
                request.Amount,
                request.DueDate,
                Enum.Parse<TransactionType>(request.Type, true),
                request.CategoryId,
                request.CreatedAt
            );

            context.Transactions.Add(transaction);
            await context.SaveChangesAsync();

            return TransactionResponse.Create(transaction);
        }

        public async Task<TransactionResponse> PaidAsync(PayTransactionRequest request)
        {
            var transaction = await FindAsync(request.Id);
            transaction.Pay(request.PaymentDate);

            context.Transactions.Update(transaction);
            await context.SaveChangesAsync();

            return TransactionResponse.Create(transaction);
        }

        public async Task<TransactionResponse> ReopenAsync(Guid id)
        {
            var transaction = await FindAsync(id);
            transaction.Reopen();

            await context.SaveChangesAsync();

            return TransactionResponse.Create(transaction);
        }

        public async Task<TransactionResponse> CancelAsync(Guid id)
        {
            var transaction = await FindAsync(id);
            transaction.Cancel();

            context.Transactions.Update(transaction);
            await context.SaveChangesAsync();

            return TransactionResponse.Create(transaction);
        }

        public async Task<TransactionResponse> GetByIdAsync(Guid id)
            => TransactionResponse.Create(await FindAsync(id));

        private async Task<Transaction> FindAsync(Guid id)
            => await context.Transactions.Include(q => q.Category).FirstOrDefaultAsync(q => q.Id == id) ?? throw new KeyNotFoundException("Transaction not found");

        private static TransactionStatus Map(TransactionStatusDto status) => status switch
        {
            TransactionStatusDto.Pending => TransactionStatus.Pending,
            TransactionStatusDto.Paid => TransactionStatus.Paid,
            TransactionStatusDto.Cancelled => TransactionStatus.Cancelled,
            _ => throw new TransactionStatusAppException()
        };

        private static TransactionType Map(TransactionTypeDto type) => type switch
        {
            TransactionTypeDto.Revenue => TransactionType.Revenue,
            TransactionTypeDto.Expense => TransactionType.Expense,
            _ => throw new TransactionTypeAppException()
        };
    }
}

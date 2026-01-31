using API.Application.DTOs.Requests;
using API.Application.DTOs.Responses;
using API.Application.Enums;
using API.Application.Exceptions;
using API.Application.Interfaces;
using API.Domain.Entities;
using API.Domain.Enums;
using API.Domain.Repositories;

namespace API.Application.Services
{
    public class TransactionAppService(ITransactionRepository repository) : ITransactionAppService
    {
        public async Task<IEnumerable<TransactionResponse>> GetAllAsync()
        {
            var transactions = await repository.GetByFilterAsync(q => q.Status != TransactionStatus.Cancelled);
            return transactions.Select(TransactionResponse.Create);
        }

        public async Task<IEnumerable<TransactionResponse>> GetByStatusAsync(TransactionStatusDto status)
        {
            var transactions = await repository.GetByFilterAsync(q => q.Status == Map(status));
            return transactions.Select(TransactionResponse.Create);
        }

        public async Task<IEnumerable<TransactionResponse>> GetByTypeAsync(TransactionTypeDto type)
        {
            var transactions = await repository.GetByFilterAsync(q => q.Type == Map(type));
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

            await repository.CreateAsync(transaction);
            return TransactionResponse.Create(transaction);
        }

        public async Task<TransactionResponse> PaidAsync(PayTransactionRequest request)
        {
            var transaction = await repository.GetByIdAsync(request.Id);
            transaction.Pay(request.PaymentDate);
            await repository.UpdateAsync(transaction);
            return TransactionResponse.Create(transaction);
        }

        public async Task<TransactionResponse> ReopenAsync(Guid id)
        {
            var transaction = await repository.GetByIdAsync(id);
            transaction.Reopen();
            await repository.UpdateAsync(transaction);
            return TransactionResponse.Create(transaction);
        }

        public async Task<TransactionResponse> CancelAsync(Guid id)
        {
            var transaction = await repository.GetByIdAsync(id);
            transaction.Cancel();
            await repository.UpdateAsync(transaction);
            return TransactionResponse.Create(transaction);
        }

        public async Task<TransactionResponse> GetByIdAsync(Guid id)
            => TransactionResponse.Create(await repository.GetByIdAsync(id));

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

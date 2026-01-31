using API.Application.DTOs.Requests;
using API.Application.DTOs.Responses;
using API.Application.Enums;
using API.Domain.Enums;

namespace API.Application.Interfaces
{
    public interface ITransactionAppService
    {
        Task<IEnumerable<TransactionResponse>> GetAllAsync();
        Task<IEnumerable<TransactionResponse>> GetByStatusAsync(TransactionStatusDto status);
        Task<IEnumerable<TransactionResponse>> GetByTypeAsync(TransactionTypeDto type);
        Task<TransactionResponse> CreateAsync(CreateTransactionRequest request);
        Task<TransactionResponse> PaidAsync(PayTransactionRequest request);
        Task<TransactionResponse> ReopenAsync(Guid id);
        Task<TransactionResponse> CancelAsync(Guid id);
        Task<TransactionResponse> GetByIdAsync(Guid id);
    }
}

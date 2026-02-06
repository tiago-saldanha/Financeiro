using Application.DTOs.Responses;
using Application.Enums;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Moq;

namespace Application.Tests.Services.TransactionAppServiceTests
{
    public class CancelAsyncTests : TransactionAppServiceBaseTests
    {
        [Fact]
        public async Task CancelAsync_WhenTransactionIsPending_ShouldMarkAsCancelled()
        {
            var transaction = Transaction.Create("Description 1", 100, Tomorrow, TransactionType.Revenue, Guid.Empty, Today);
            _repositoryMock.Setup(r => r.GetByIdAsync(transaction.Id)).ReturnsAsync(transaction);
            
            var result = await _service.CancelAsync(transaction.Id);

            _repositoryMock.Verify(r => r.Update(transaction), Times.Once);
            _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
            Assert.IsType<TransactionResponse>(result);
            Assert.Equal(transaction.Id, result.Id);
            Assert.Null(result.PaymentDate);
            Assert.Equal(TransactionStatusDto.cancelled.ToString(), result.Status.ToLower());
        }

        [Fact]
        public async Task CancelAsync_WhenTransactionIsPaid_ShouldThrowTransactionCancelException()
        {
            var transaction = Transaction.Create("Description 1", 100, Tomorrow, TransactionType.Revenue, Guid.Empty, Today);
            transaction.Pay(Tomorrow);
            _repositoryMock.Setup(r => r.GetByIdAsync(transaction.Id)).ReturnsAsync(transaction);

            await Assert.ThrowsAsync<TransactionCancelException>(() => _service.CancelAsync(transaction.Id));
            Assert.Equal(TransactionStatus.Paid, transaction.Status);
            Assert.Equal(Tomorrow, transaction.PaymentDate);
        }
    }
}

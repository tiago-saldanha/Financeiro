using API.Application.DTOs.Requests;
using API.Application.DTOs.Responses;

namespace API.Application.Interfaces
{
    public interface ICategoryAppService
    {
        Task<IEnumerable<CategoryResponse>> GetAllAsync();
        Task<CategoryResponse> GetByIdAsync(Guid id);
        Task<CategoryResponse> CreateAsync(CategoryRequest request);
    }
}

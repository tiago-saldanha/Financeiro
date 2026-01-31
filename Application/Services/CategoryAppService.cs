using Domain.Entities;
using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;

namespace Application.Services
{
    public class CategoryAppService(AppDbContext context) : ICategoryAppService
    {
        public async Task<IEnumerable<CategoryResponse>> GetAllAsync()
        {
            var categories = await context.Categories.Include(q => q.Transactions).AsNoTracking().ToListAsync();
            return categories.Select(CategoryResponse.Create);
        }

        public async Task<CategoryResponse> GetByIdAsync(Guid id)
        {
            var category = await context.Categories
                .AsNoTracking()
                .Include(q => q.Transactions)
                .FirstOrDefaultAsync(q => q.Id == id) ?? throw new KeyNotFoundException("Category not found");
            
            return CategoryResponse.Create(category);
        }

        public async Task<CategoryResponse> CreateAsync(CategoryRequest request)
        {
            if (string.IsNullOrEmpty(request.Name)) throw new InvalidOperationException("Name is required");
            
            var category = new Category(Guid.NewGuid(), request.Name, request.Description);
            context.Categories.Add(category);
            await context.SaveChangesAsync();
            
            return CategoryResponse.Create(category);
        }
    }
}

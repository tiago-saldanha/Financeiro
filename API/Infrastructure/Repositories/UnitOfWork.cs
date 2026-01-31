using API.Data;
using API.Domain.Repositories;

namespace API.Infrastructure.Repositories
{
    public class UnitOfWork(AppDbContext context) : IUnitOfWork
    {
        public async Task CommitAsync() => await context.SaveChangesAsync();
    }
}

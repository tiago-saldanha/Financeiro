using Domain.Repositories;
using Infrastructure.Data;

namespace Infraestructure.Repositories
{
    public class UnitOfWork(AppDbContext context) : IUnitOfWork
    {
        public async Task CommitAsync() => await context.SaveChangesAsync();
    }
}

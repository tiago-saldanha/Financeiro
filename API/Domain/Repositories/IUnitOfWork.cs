namespace API.Domain.Repositories
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
    }
}

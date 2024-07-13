using AI.Core.Entities;

namespace AI.Core.Interfaces.Repositories
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : Person;
        Task<int> CompletesAsync();
    }
}

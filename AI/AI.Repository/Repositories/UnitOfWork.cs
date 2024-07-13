using AI.Core.Entities;
using AI.Core.Interfaces.Repositories;
using AI.Repository.Data;
using System.Collections;

namespace AI.Repository.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        private readonly Hashtable _repositories;
        public UnitOfWork(DataContext context)
        {
            _context = context;
            _repositories = new Hashtable();
        }
        public async Task<int> CompletesAsync() => await _context.SaveChangesAsync();
        public async ValueTask DisposeAsync() => await _context.DisposeAsync();
        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : Person
        {
            var typeName = typeof(TEntity).Name;
            if (!_repositories.ContainsKey(typeName))
            {
                var repo = new GenericRepository<TEntity>(_context);
                _repositories.Add(typeName, repo);
                return repo;
            }
            return (_repositories[typeName] as GenericRepository<TEntity>)!;
        }
    }
}

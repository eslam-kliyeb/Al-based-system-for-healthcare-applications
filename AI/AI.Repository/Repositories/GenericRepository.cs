using AI.Core.Entities;
using AI.Core.Interfaces.Repositories;
using AI.Core.Specification;
using AI.Repository.Data;
using AI.Repository.Specification;
using Microsoft.EntityFrameworkCore;

namespace AI.Repository.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : Person
    {
        private readonly DataContext _dataContext;
        public GenericRepository(DataContext dataContext)
        { // ask CLR for Creating Object From DbContext Implicitly
            _dataContext = dataContext;
        }

        #region WithOut Spec
        public async Task<IReadOnlyList<TEntity>> GetAllAsync()
        {
            return await _dataContext.Set<TEntity>().ToListAsync();
        }
        public async Task<TEntity> GetByIdAsync(int id)
        {
            //return await _dataContext.Set<T>().Where(x => x.Id == id).FirstOrDefaultAsync();
            var x = await _dataContext.Set<TEntity>().FindAsync(id);
            return x;
        }
        public async Task<TEntity> GetByEmailAsync(string Email)
        {
            var x = await _dataContext.Set<TEntity>().FindAsync(Email);
            return x;
        }
        public async Task AddAsync(TEntity entity) => await _dataContext.Set<TEntity>().AddAsync(entity);
        #endregion

        #region With Spec
        public async Task<IReadOnlyList<TEntity>> GetAllWithSpecAsync(ISpecifications<TEntity> Spec)
        {
            return await ApplySpecification(Spec).ToListAsync();
        }
        public async Task<TEntity> GetByIdWithSpecAsync(ISpecifications<TEntity> Spec)
        {
            return (await ApplySpecification(Spec).FirstOrDefaultAsync())!;
        }
        public async Task<int> GetPatientCountWithSpecAsync(ISpecifications<TEntity> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }
        #endregion

        public void Delete(TEntity entity) => _dataContext.Set<TEntity>().Remove(entity);
        public void Update(TEntity entity) => _dataContext.Set<TEntity>().Update(entity);

        private IQueryable<TEntity> ApplySpecification(ISpecifications<TEntity> Spec)
        {
            return SpecificationEvaluator<TEntity>.GetQuery(_dataContext.Set<TEntity>(), Spec);
        }

    }
}

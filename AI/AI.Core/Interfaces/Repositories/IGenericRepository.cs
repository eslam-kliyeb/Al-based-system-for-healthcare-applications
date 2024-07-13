using AI.Core.Entities;
using AI.Core.Specification;

namespace AI.Core.Interfaces.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : Person
    {
        #region With Out Spec
        //Get All
        Task<IReadOnlyList<TEntity>> GetAllAsync();
        //Get by Id
        Task<TEntity> GetByIdAsync(int id);
        Task AddAsync(TEntity entity);

        //Get by Email
        Task<TEntity> GetByEmailAsync(string Email);
        #endregion

        #region With Spec
        Task<IReadOnlyList<TEntity>> GetAllWithSpecAsync(ISpecifications<TEntity> Spec);
        Task<TEntity> GetByIdWithSpecAsync(ISpecifications<TEntity> Spec);
        Task<int> GetPatientCountWithSpecAsync(ISpecifications<TEntity> specification);
        #endregion

        void Delete(TEntity entity);
        void Update(TEntity entity);
    }
}

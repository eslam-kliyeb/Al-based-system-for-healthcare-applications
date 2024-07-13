using AI.Core.Entities;
using AI.Core.Specification;
using Microsoft.EntityFrameworkCore;

namespace AI.Repository.Specification
{
    public static class SpecificationEvaluator<T> where T : Person
    {
        //Fun To Build Query
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecifications<T> Spec)
        {
            var query = inputQuery; // _dataContext.Set<T>()
            if (Spec.Criteria != null)
            {
                query = query.Where(Spec.Criteria);
            }
            if (Spec.OrderBy is not null)
            {
                query = query.OrderBy(Spec.OrderBy);
            }
            if (Spec.OrderByDesc is not null)
            {
                query = query.OrderByDescending(Spec.OrderByDesc);
            }
            if (Spec.IsPaginated)
            {
                query = query.Skip(Spec.Skip).Take(Spec.Take);
            }
            foreach (var item in Spec.Includes)
            {
                query = query.Include(item);
            }
            return query;
        }
    }
}

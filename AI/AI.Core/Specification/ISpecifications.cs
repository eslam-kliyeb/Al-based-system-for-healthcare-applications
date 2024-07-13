using AI.Core.Entities;
using System.Linq.Expressions;

namespace AI.Core.Specification
{
    public interface ISpecifications<T> where T : Person
    {
        // Sign Fro Property For Where Condition [Where(p=>p.Id==id)]
        public Expression<Func<T,bool>> Criteria { get; set; }
        // Sign Fro Property For List Of include [Include(p=>p.Doctor==id)]
        public List<Expression<Func<T,Object>>> Includes { get; set; }
        //orderby
        public Expression<Func<T, object>> OrderBy { get; }
        public Expression<Func<T, object>> OrderByDesc { get; }
        //skip
        public int Skip { get; }
        public int Take { get; }
        public bool IsPaginated { get; }
    }
}

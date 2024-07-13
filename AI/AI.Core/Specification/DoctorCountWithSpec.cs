using AI.Core.Entities;

namespace AI.Core.Specification
{
    public class DoctorCountWithSpec : BaseSpecifications<Doctor>
    {
        public DoctorCountWithSpec(SpecificationParameters specs)
              : base(p =>(string.IsNullOrWhiteSpace(specs.Search) || p.Name.ToLower().Contains(specs.Search)))
        {
        }
    }
}

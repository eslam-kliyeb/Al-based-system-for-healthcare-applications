using AI.Core.Entities;

namespace AI.Core.Specification
{
    public class DoctorSpecifications : BaseSpecifications<Doctor>
    {
        public DoctorSpecifications(SpecificationParameters specs)
               : base(p =>(string.IsNullOrWhiteSpace(specs.Search) || p.Name.ToLower().Contains(specs.Search)))
        {
            ApplyPagination(specs.PageSize, specs.PageIndex);
            if (specs.Sort is not null)
            {
                switch (specs.Sort)
                {
                    case SortingParameters.NameAsc:
                        OrderBy = x => x.Name;
                        break;
                    case SortingParameters.NameDesc:
                        OrderByDesc = x => x.Name;
                        break;
                    case SortingParameters.AgeAsc:
                        OrderBy = x => x.Age;
                        break;
                    case SortingParameters.AgeDesc:
                        OrderByDesc = x => x.Age;
                        break;
                    default:
                        OrderBy = x => x.Name;
                        break;
                }
            }
            else
            {
                OrderBy = x => x.Name;
            }
        }
    }
}

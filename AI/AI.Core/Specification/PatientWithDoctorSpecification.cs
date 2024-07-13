using AI.Core.Entities;

namespace AI.Core.Specification
{
    public class PatientWithDoctorSpecifications : BaseSpecifications<Patient> 
    {
        //CTOR Is Used For Get ALL Patients
        public PatientWithDoctorSpecifications(string DoctorEmail, SpecificationParameters specs) 
               : base(p => p.DoctorEmail == DoctorEmail &&
               (string.IsNullOrWhiteSpace(specs.Search) || p.Name.ToLower().Contains(specs.Search)))
        {
            Includes.Add(p => p.Doctor);
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
        //CTOR Is Used For Get Patient
        public PatientWithDoctorSpecifications(string DoctorEmail, int PatientId) : base(p => p.DoctorEmail == DoctorEmail && p.Id==PatientId)
        {
            Includes.Add(p => p.Doctor);
        }
    }
}

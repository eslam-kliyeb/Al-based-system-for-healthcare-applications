using AI.Core.Entities;

namespace AI.Core.Specification
{
    public class PatientCountWithSpec : BaseSpecifications<Patient>
    {
        public PatientCountWithSpec(string DoctorId, SpecificationParameters specs)
               : base(p => p.DoctorEmail == DoctorId &&
               (string.IsNullOrWhiteSpace(specs.Search) || p.Name.ToLower().Contains(specs.Search)))
        {
        }
    }
}

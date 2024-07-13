using AI.Core.Specification;
using AI.Core.DTOs;
namespace AI.Core.Interfaces.Service
{
    public interface IPatientService
    {
        Task<PaginatedResultDto<PatientToReturnDto>> GetAllPatientsAsync(string DoctorEmail, SpecificationParameters specificationParameters);
        Task<PatientToReturnDto> GetPatientAsync(string DoctorEmail, int PatientId);
        Task<int> AddPatientAsync(string DoctorEmail, PatientInputDto patientInputDto);
        Task<int> DeletePatient(string DoctorEmail, int PatientId);
    }
}

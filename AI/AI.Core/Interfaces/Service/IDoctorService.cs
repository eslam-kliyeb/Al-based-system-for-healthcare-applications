using AI.Core.DTOs;
using AI.Core.Specification;

namespace AI.Core.Interfaces.Service
{
    public interface IDoctorService
    {
        Task<DoctorDto> GetDoctorAsync(string DoctorEmail);
        Task<PaginatedResultDto<DoctorDto>> GetAllDoctorsAsync(SpecificationParameters specificationParameters);
        Task<int> AddDoctorAsync(DoctorDto DoctorDto);
        Task<int> DeleteDoctor(string DoctorEmail);
    }
}

using AI.Core.DTOs;
using AI.Core.Entities;
using AI.Core.Interfaces.Repositories;
using AI.Core.Interfaces.Service;
using AI.Core.Specification;
using AutoMapper;

namespace AI.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DoctorService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<PaginatedResultDto<DoctorDto>> GetAllDoctorsAsync(SpecificationParameters specificationParameters)
        {
            var spec = new DoctorSpecifications(specificationParameters);
            var specCount = new DoctorCountWithSpec(specificationParameters);
            var Patients = await _unitOfWork.Repository<Doctor>().GetAllWithSpecAsync(spec);
            var count = await _unitOfWork.Repository<Doctor>().GetPatientCountWithSpecAsync(specCount);
            var mappedPatient = _mapper.Map<IReadOnlyList<DoctorDto>>(Patients);
            return new PaginatedResultDto<DoctorDto>
            {
                Data = mappedPatient,
                PageIndex = specificationParameters.PageIndex,
                PageSize = specificationParameters.PageSize,
                TotalCount = count,
            };
        }
        public async Task<DoctorDto> GetDoctorAsync(string DoctorEmail)
        {
            var Patient = await _unitOfWork.Repository<Doctor>().GetByEmailAsync(DoctorEmail);
            var MappedDoctor = _mapper.Map<DoctorDto>(Patient);
            return (MappedDoctor);
        }
        public async Task<int> AddDoctorAsync(DoctorDto DoctorDto)
        {
            var Doctor = _mapper.Map<Doctor>(DoctorDto);
            await _unitOfWork.Repository<Doctor>().AddAsync(Doctor);
            int x = await _unitOfWork.CompletesAsync();
            return x;
        }
        public async Task<int> DeleteDoctor(string DoctorEmail)
        {
            var Doctor = await _unitOfWork.Repository<Doctor>().GetByEmailAsync(DoctorEmail);
            int x = 0;
            if (Doctor != null)
            {
                _unitOfWork.Repository<Doctor>().Delete(Doctor);
                x = await _unitOfWork.CompletesAsync();
            }
            return (x);
        }
    }
}

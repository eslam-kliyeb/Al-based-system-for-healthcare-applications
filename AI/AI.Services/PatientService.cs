using AI.Core.DTOs;
using AI.Core.Entities;
using AI.Core.Interfaces.Repositories;
using AI.Core.Interfaces.Service;
using AI.Core.Specification;
using AI.Repository.Utility;
using AutoMapper;
namespace AI.Services
{
    public class PatientService : IPatientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClassifierService _classifierService;
        private readonly ISegmentationService _segmentationService;
        public PatientService(IUnitOfWork unitOfWork, IMapper mapper, IClassifierService classifierService, ISegmentationService segmentationService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _classifierService = classifierService;
            _segmentationService = segmentationService;
        }
        public async Task<int> AddPatientAsync(string DoctorEmail, PatientInputDto patientInputDto)
        {
            var patient = _mapper.Map<Patient>(patientInputDto);
            patient.MriUrl = DocumentSettings.UploadFile(patientInputDto.MriImage, "Mri");
            patient.MriUrl = patient.MriUrl.Substring(58);
            patient.segmentUrl = await _segmentationService.Segmentation(patientInputDto.MriImage);
            patient.Diagnosed = await _classifierService.Classifier(patientInputDto.MriImage);
            patient.DoctorEmail = DoctorEmail;
            await _unitOfWork.Repository<Patient>().AddAsync(patient);
            int x = 0;
            x = await _unitOfWork.CompletesAsync();
            return x;
        }
        public async Task<int> DeletePatient(string DoctorEmail, int PatientId)
        {
            var spec = new PatientWithDoctorSpecifications(DoctorEmail, PatientId);
            var Patient = await _unitOfWork.Repository<Patient>().GetByIdWithSpecAsync(spec);
            int x = 0;
            if (Patient != null)
            {
                _unitOfWork.Repository<Patient>().Delete(Patient);
                x = await _unitOfWork.CompletesAsync();
            }
            return (x);
        }
        public async Task<PaginatedResultDto<PatientToReturnDto>> GetAllPatientsAsync(string DoctorEmail, SpecificationParameters specificationParameters)
        {
            var spec = new PatientWithDoctorSpecifications(DoctorEmail, specificationParameters);
            var specCount = new PatientCountWithSpec(DoctorEmail, specificationParameters);
            var Patients = await _unitOfWork.Repository<Patient>().GetAllWithSpecAsync(spec);
            var count = await _unitOfWork.Repository<Patient>().GetPatientCountWithSpecAsync(specCount);
            var mappedPatient = _mapper.Map<IReadOnlyList<PatientToReturnDto>>(Patients);
            return new PaginatedResultDto<PatientToReturnDto>
            {
                Data = mappedPatient,
                PageIndex = specificationParameters.PageIndex,
                PageSize = specificationParameters.PageSize,
                TotalCount = count,
            };
        }
        public async Task<PatientToReturnDto> GetPatientAsync(string DoctorEmail, int PatientId)
        {
            var spec = new PatientWithDoctorSpecifications(DoctorEmail, PatientId);
            var Patient = await _unitOfWork.Repository<Patient>().GetByIdWithSpecAsync(spec);
            var MappedPatient = _mapper.Map<PatientToReturnDto>(Patient);
            return (MappedPatient);
        }
    }
}
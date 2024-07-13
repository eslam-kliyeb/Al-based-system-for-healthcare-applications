using AI.Core.DTOs;
using AI.Core.Entities;
using AI.Core.Interfaces.Service;
using AI.Core.Specification;
using AI.Errors;
using AI.Helper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AI.Controllers
{
    public class PatientController : APIBaseController
    {
        private readonly IPatientService _PatientService;
        public PatientController(IPatientService PatientService)
        {
            _PatientService = PatientService;
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("GetAllPatients")]
        [Cash(60)]
        public async Task<ActionResult<IReadOnlyList<Patient>>> GetPatients(string DoctorEmail, [FromQuery] SpecificationParameters specificationParameters)
        {
            return Ok(await _PatientService.GetAllPatientsAsync(DoctorEmail, specificationParameters));
        }
        [HttpGet("GetPatientById")]
        [Cash(60)]
        public async Task<ActionResult<PatientToReturnDto>> GetPatient(string DoctorEmail, int PatientId)
        {
            var Patient = await _PatientService.GetPatientAsync(DoctorEmail, PatientId);
            return Patient is not null ? Ok(Patient) : NotFound(new ApiResponse(400, $"Patient With Id {PatientId} Not Found"));
        }
        [HttpPost("AddPatient")]
        public async Task<ActionResult> AddPatient(string DoctorEmail, [FromQuery] PatientInputDto patientInputDto)
        {
            int x = await _PatientService.AddPatientAsync(DoctorEmail, patientInputDto);
            return x == 1 ? Ok("Added is Done") : NotFound(new ApiResponse(400, $"Not Added"));
        }
        [HttpDelete("DeletePatient")]
        public async Task<ActionResult> DeletePatient(string DoctorEmail, int PatientId)
        {
            int product = await _PatientService.DeletePatient(DoctorEmail, PatientId);
            return product == 1 ? Ok("Deletion is Done") : NotFound(new ApiResponse(400, $"Patient With Id = {PatientId} Is Not found"));
        }
    }
}
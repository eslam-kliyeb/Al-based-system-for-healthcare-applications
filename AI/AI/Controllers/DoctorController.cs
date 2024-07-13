using AI.Core.DTOs;
using AI.Core.Entities;
using AI.Core.Interfaces.Service;
using AI.Core.Specification;
using AI.Errors;
using AI.Helper;
using Microsoft.AspNetCore.Mvc;

namespace AI.Controllers
{
    public class DoctorController : APIBaseController
    {
        private readonly IDoctorService _DoctorService;
        public DoctorController(IDoctorService DoctorService)
        {
            _DoctorService = DoctorService;
        }
        [HttpGet("GetAllDoctor")]
        [Cash(60)]
        public async Task<ActionResult<IReadOnlyList<Doctor>>> GetDoctors([FromQuery] SpecificationParameters specificationParameters)
        {
            return Ok(await _DoctorService.GetAllDoctorsAsync(specificationParameters));
        }

        [HttpGet("GetDoctorById")]
        [Cash(60)]
        public async Task<ActionResult<DoctorDto>> GetDoctor(string DoctorEmail)
        {
            var product = await _DoctorService.GetDoctorAsync(DoctorEmail);
            return product is not null ? Ok(product) : NotFound(new ApiResponse(400, $"Doctor With Id {DoctorEmail} Not Found"));
        }
        [HttpPost("AddDoctor")]
        public async Task<ActionResult> AddDoctor([FromQuery] DoctorDto DoctorDto)
        {
            int x = await _DoctorService.AddDoctorAsync(DoctorDto);
            return x == 1 ? Ok("Added is Done") : NotFound(new ApiResponse(400, $"Not Added"));
        }
        [HttpDelete("DeleteDoctor")]
        public async Task<ActionResult> DeleteDoctor(string DoctorEmail)
        {
            int product = await _DoctorService.DeleteDoctor(DoctorEmail);
            return product == 1 ? Ok("Deletion is Done") : NotFound(new ApiResponse(400, $"Doctor With Id {DoctorEmail} Is Not Deleted"));
        }
    }
}

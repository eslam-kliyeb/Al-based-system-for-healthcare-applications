using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace AI.Core.DTOs
{
    public class PatientInputDto
    {
        [MaxLength(30), MinLength(5)]
        [Required(ErrorMessage = "Name is Required")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Country is Required")]
        public string? Country { get; set; }
        [Required(ErrorMessage = "City is Required")]
        public string? City { get; set; }
        [Phone]
        public string? Phone { get; set; }
        [Required(ErrorMessage = "Age is Required")]
        public int? Age { get; set; }
        [Required(ErrorMessage = "Mri is Required")]
        public IFormFile MriImage { get; set; }
    }
}

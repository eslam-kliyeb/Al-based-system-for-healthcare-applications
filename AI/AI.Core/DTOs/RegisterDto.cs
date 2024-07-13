using System.ComponentModel.DataAnnotations;

namespace AI.Core.DTOs
{
    public class RegisterDto
    {
        [Required, MaxLength(50)]
        public string DisplayName { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required,MaxLength(11)]
        [Phone]
        public string PhoneNumber { get; set; }
        [Required, MaxLength(50)]
        public string City { get; set; }
        [Required, MaxLength(50)]
        public string Street { get; set; }
        [Required, MaxLength(50)]
        public string Country { get; set; }
        [Required, MaxLength(50)]
        public string Qualification { get; set; }
        [Required]
        [Range(18, 65, ErrorMessage = "Age must be between 18 and 65")]
        public int Age { get; set; }
        [Required]
        [RegularExpression("^(?=.*\\d)(?=.*[#$@!%&*?])[A-Za-z\\d#$@!%&*?]{8,}$",
        ErrorMessage = "Min 1 special character Min 1 number Min 8 characters or More")]
        public string Password { get; set; }
    }
}

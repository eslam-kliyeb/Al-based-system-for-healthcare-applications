using AI.Core.DTOs;
using System.ComponentModel.DataAnnotations;

namespace AdminDashboard.Models
{
    public class ClassificationViewModel
    {
        [Required(ErrorMessage = "No Image")]
        public IFormFile Image { get; set; }
        public string answer { get; set; }
        public LoginDto loginDto { get; set; }
    }
}

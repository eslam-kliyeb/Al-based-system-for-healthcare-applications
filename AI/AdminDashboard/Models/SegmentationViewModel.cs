using AI.Core.DTOs;
using System.ComponentModel.DataAnnotations;

namespace AdminDashboard.Models
{
    public class SegmentationViewModel
    {
        [Required(ErrorMessage = "No Image")]
        public IFormFile Image { get; set; }
        public string ImageName { get; set; }
        public LoginDto loginDto { get; set; }

    }
}

using AI.Core.DTOs;
using System.ComponentModel.DataAnnotations;

namespace AdminDashboard.Models
{
    public class ChatBotViewModel
    {
        [Required(ErrorMessage = "Empty message")]
        public string message {  get; set; }
        public string answer { get; set; }
        public LoginDto loginDto { get; set; }
    }
}

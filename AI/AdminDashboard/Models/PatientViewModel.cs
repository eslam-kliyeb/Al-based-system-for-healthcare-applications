using AI.Core.DTOs;
using AI.Core.Entities;

namespace AdminDashboard.Models
{
	public class PatientViewModel
	{
		public LoginDto LoginDto { get; set; }
		public IReadOnlyList<PatientToReturnDto> Patient { get; set; } // Assuming 'Patient' is the type for patient data
	}
}

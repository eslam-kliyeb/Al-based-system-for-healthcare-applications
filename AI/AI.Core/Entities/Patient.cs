
namespace AI.Core.Entities
{
    public class Patient:Person
    {
        public string Diagnosed {  get; set; }
        public string MriUrl { get; set; }
        public string segmentUrl { get; set; }
        //==================================
        public Doctor? Doctor { get; set; }
        public string? DoctorEmail {  get; set; }
    }
}

namespace AI.Core.DTOs
{
    public class PatientToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public int Age { get; set; }
        public string Diagnosed { get; set; }
        public string MriUrl { get; set; }
        public string segmentUrl { get; set; }
    }
}

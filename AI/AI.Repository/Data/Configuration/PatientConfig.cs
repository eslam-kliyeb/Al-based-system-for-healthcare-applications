using AI.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AI.Repository.Data.Configuration
{
    public class PatientConfig : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.HasOne(patient => patient.Doctor)
                   .WithMany()
                   .HasForeignKey(patient => patient.DoctorEmail);
        }
    }
}

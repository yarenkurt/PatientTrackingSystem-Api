using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mappings.EF
{
    public class DoctorPatientMap : IEntityTypeConfiguration<DoctorPatient>
    {
        public void Configure(EntityTypeBuilder<DoctorPatient> builder)
        {
            builder.ToTable("DoctorPatients");
            builder.HasIndex(x => new {x.PatientId, x.DoctorId}).IsUnique();
        }
    }
}
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mappings.EF
{
    public class PatientMap : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("Patients");
            builder.HasOne(x => x.Person).WithMany(x => x.Patients).HasForeignKey(x => x.PersonId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasIndex(x => x.PersonId).IsUnique();
            builder.HasIndex(x => x.IdentityNumber).IsUnique();
            
        }
    }
}
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mappings.EF
{
    public class DoctorMap : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.ToTable("Doctors");
            // builder.HasOne(x => x.Person).WithMany(x => x.Doctors).HasForeignKey(x => x.PersonId)
            //     .OnDelete(DeleteBehavior.Cascade);
            builder.HasIndex(x => x.PersonId).IsUnique();
            builder.HasIndex(x => x.Email).IsUnique();
            
        }
    }
}
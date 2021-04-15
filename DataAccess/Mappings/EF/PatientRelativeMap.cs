using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mappings.EF
{
    public class PatientRelativeMap : IEntityTypeConfiguration<PatientRelative>
    {
        public void Configure(EntityTypeBuilder<PatientRelative> builder)
        {
            builder.ToTable("PatientRelatives");
        }
    }
}
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mappings.EF
{
    public class PatientDiseaseMap : IEntityTypeConfiguration<PatientDisease>
    {
        public void Configure(EntityTypeBuilder<PatientDisease> builder)
        {
            builder.ToTable("PatientDiseases");
            builder.HasIndex(x => new {x.PatientId, x.DiseaseId}).IsUnique();
        }
    }
}
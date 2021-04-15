using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mappings.EF
{
    public class RelativeDegreeMap : IEntityTypeConfiguration<RelativeDegree>
    {
        public void Configure(EntityTypeBuilder<RelativeDegree> builder)
        {
            builder.ToTable("RelativeDegrees");
        }
    }

}
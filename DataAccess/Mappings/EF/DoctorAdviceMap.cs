using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mappings.EF
{
    public class DoctorAdviceMap : IEntityTypeConfiguration<DoctorAdvice>
    {
        public void Configure(EntityTypeBuilder<DoctorAdvice> builder)
        {
            builder.ToTable("DoctorAdvices");
        }
    }
}
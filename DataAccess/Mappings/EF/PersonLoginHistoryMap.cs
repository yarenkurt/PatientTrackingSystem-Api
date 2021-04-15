using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mappings.EF
{
    public class PersonLoginHistoryMap : IEntityTypeConfiguration<PersonLoginHistory>
    {
        public void Configure(EntityTypeBuilder<PersonLoginHistory> builder)
        {
            builder.ToTable("PersonLoginHistories");
           // builder.HasIndex(x => x.IpAddress).IsUnique();
        }
    }
}
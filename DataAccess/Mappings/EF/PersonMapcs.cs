using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mappings.EF
{
    public class PersonMap : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("Persons");
            builder.HasIndex(x => x.Gsm).IsUnique();
            builder.Property(x => x.UserName).HasMaxLength(100);
            builder.HasIndex(x => x.UserName).IsUnique();
        }
    }
}
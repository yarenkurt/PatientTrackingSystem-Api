using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mappings.EF
{
    public class QuestionPoolMap : IEntityTypeConfiguration<QuestionPool>
    {
        public void Configure(EntityTypeBuilder<QuestionPool> builder)
        {
            builder.ToTable("QuestionPools");
        }
    }
}
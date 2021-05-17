using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mappings.EF
{
    public class AnswerPoolMap :  IEntityTypeConfiguration<AnswerPool>
    {
        public void Configure(EntityTypeBuilder<AnswerPool> builder)
        {
            builder.ToTable("AnswerPools");
            builder.HasOne(x => x.QuestionPool).WithMany(x => x.Answers).HasForeignKey(x => x.QuestionPoolId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

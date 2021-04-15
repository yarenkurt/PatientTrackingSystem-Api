using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mappings.EF
{
    public class PatientQuestionMap : IEntityTypeConfiguration<PatientQuestion>
    {
        public void Configure(EntityTypeBuilder<PatientQuestion> builder)
        {
            builder.ToTable("PatientQuestions");
        }
    }
}
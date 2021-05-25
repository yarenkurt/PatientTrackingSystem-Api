using Core.Enums;

namespace Entities.Dtos
{
    public class GetPatientQuestionDto
    {
        public int Id { get; set; }
        public string QuestionDesc { get; set; }
        public string PatientName { get; set; }
        public QuestionType QuestionType { get; set; }
    }
}
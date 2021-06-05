namespace Entities.Dtos
{
    public class InsertPatientAnswerDto
    {
        public int QuestionId { get; set; }
        public decimal Score { get; set; }
        public string AnswerDescription { get; set; }
    }
}
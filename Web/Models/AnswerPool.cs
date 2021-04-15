using System.Text.Json.Serialization;

namespace Web.Models
{
    public class AnswerPool
    {
        public int Id { get; set; }
        public int QuestionPoolId { get; set; }
        public string Description { get; set; }
        public decimal Score { get; set; }

        [JsonIgnore]
        public QuestionPool QuestionPool { get; set; }
        
    }
}
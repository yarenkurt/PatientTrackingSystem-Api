using System.Text.Json.Serialization;
using Core.Signatures;

namespace Entities.Concrete
{
    public class AnswerPool : IBaseEntity
    {
        public int Id { get; set; }
        public int QuestionPoolId { get; set; }
        public string Description { get; set; }
        public decimal Score { get; set; }
        public bool IsActive { get; set; } = true;

        [JsonIgnore]
        public QuestionPool QuestionPool { get; set; }
        
    }
}
using System;
using System.Text.Json.Serialization;
using Core.Signatures;

namespace Entities.Concrete
{
    public class PatientAnswer : IBaseEntity
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int QuestionPoolId { get; set; }
        public decimal Score { get; set; }
        public string AnswerDescription { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; } = true;
        
        [JsonIgnore]
        public bool Result { get; set; }

        [JsonIgnore]
        public Patient Patient { get; set; }
        [JsonIgnore]
        public QuestionPool QuestionPool { get; set; }
    }
}
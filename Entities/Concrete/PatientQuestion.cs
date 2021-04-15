using System;
using System.Text.Json.Serialization;
using Core.Signatures;

namespace Entities.Concrete
{
    public class PatientQuestion : IBaseEntity
    {
        public int Id { get; set; }
        public int QuestionPoolId { get; set; }
        public int PatientId { get; set; }
        public int Day { get; set; }
        public DateTime CreatedAt { get; set; }

        [JsonIgnore]
        public QuestionPool QuestionPool { get; set; }
        [JsonIgnore]
        public Patient Patient { get; set; }
    }
}
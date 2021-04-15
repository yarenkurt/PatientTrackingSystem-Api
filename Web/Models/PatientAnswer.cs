using System;
using System.Text.Json.Serialization;

namespace Web.Models
{
    public class PatientAnswer 
    {
        public int Id { get; set; }
        [JsonIgnore]
        public int PatientId { get; set; }
        public int QuestionPoolId { get; set; }
        public decimal Score { get; set; }
        public DateTime CreatedAt { get; set; }

        [JsonIgnore]
        public Patient Patient { get; set; }
        [JsonIgnore]
        public QuestionPool QuestionPool { get; set; }
    }
}
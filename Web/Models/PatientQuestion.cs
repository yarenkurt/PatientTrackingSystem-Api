using System;
using System.Text.Json.Serialization;

namespace Web.Models
{
    public class PatientQuestion 
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
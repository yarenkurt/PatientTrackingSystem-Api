using System;
using System.Text.Json.Serialization;

namespace Web.Models
{
    public class DoctorAdvice 
    {
        public int Id { get; set; }
        public string Description { get; set; }
    
        public string CreatedUserName { get; set; } //Claimsten alınacak
        public int PatientId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ReadingTime { get; set; }

    }
}
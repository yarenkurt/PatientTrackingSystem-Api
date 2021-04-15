using System.Text.Json.Serialization;

namespace Web.Models
{
    public class PatientDisease 
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DiseaseId { get; set; }
    }
}
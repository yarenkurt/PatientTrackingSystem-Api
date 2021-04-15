using System.Text.Json.Serialization;

namespace Web.Models
{
    public class PatientRelative 
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gsm { get; set; }
    }
}
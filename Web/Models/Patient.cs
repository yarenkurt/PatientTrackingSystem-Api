using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Web.Models
{
    public class Patient 
    {
        public int Id { get; set; }
        public string IdentityNumber { get; set; }
        public string Email { get; set; }
        public int PersonId { get; set; }

        public Person Person { get; set; }
        
        [JsonIgnore]
        public ICollection<PatientDisease> PatientDiseases { get; set; }
    }

}
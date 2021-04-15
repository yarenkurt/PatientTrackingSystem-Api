using System.Text.Json.Serialization;
using Core.Signatures;

namespace Entities.Concrete
{
    public class PatientRelative : IBaseEntity
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gsm { get; set; }

        [JsonIgnore]
        public Patient Patient { get; set; }
    }
}
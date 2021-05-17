using System.Text.Json.Serialization;
using Core.Signatures;

namespace Entities.Concrete
{
    public class PatientDisease : IBaseEntity
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DiseaseId { get; set; }

        public bool IsActive { get; set; }
        [JsonIgnore]
        public Patient Patient { get; set; }
        [JsonIgnore]
        public Disease Disease { get; set; }
    }
}
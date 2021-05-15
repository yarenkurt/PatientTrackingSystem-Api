using System.Collections.Generic;
using Core.Signatures;
using Newtonsoft.Json;

namespace Entities.Concrete
{
    public class Patient : IBaseEntity
    {
        public int Id { get; set; }
        public string IdentityNumber { get; set; }
        public string Email { get; set; }
        public int PersonId { get; set; }
        public bool IsBlocked { get; set; }
        
        [JsonIgnore]
        public Person Person { get; set; }
        
        [JsonIgnore]
        public ICollection<PatientDisease> PatientDiseases { get; set; }
    }

}
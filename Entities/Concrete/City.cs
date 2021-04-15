using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Core.Signatures;

namespace Entities.Concrete
{
    public class City : IBaseEntity
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int CountryId { get; set; }

        [JsonIgnore]
        public Country Country { get; set; }
        
        [JsonIgnore]
        public ICollection<District> Districts { get; set; }
    }
}
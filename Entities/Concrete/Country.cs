using System.Collections.Generic;
using System.Text.Json.Serialization;
using Core.Signatures;

namespace Entities.Concrete
{
    public class Country : IBaseEntity
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string CountryCode { get; set; }

        [JsonIgnore]
        public ICollection<City> Cities { get; set; }
    }
}
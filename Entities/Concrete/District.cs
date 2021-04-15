using System.Text.Json.Serialization;
using Core.Signatures;

namespace Entities.Concrete
{
    public class District : IBaseEntity
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int CityId { get; set; }

        [JsonIgnore]
        public City City { get; set; }
    }
}
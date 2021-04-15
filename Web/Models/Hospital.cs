using System.Text.Json.Serialization;

namespace Web.Models
{
    public class Hospital
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        public int DistrictId { get; set; }
        
        [JsonIgnore]
        public District District { get; set; }
    }
}
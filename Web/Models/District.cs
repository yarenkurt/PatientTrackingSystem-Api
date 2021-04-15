using System.Text.Json.Serialization;

namespace Web.Models
{
    public class District 
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int CityId { get; set; }
    }
}
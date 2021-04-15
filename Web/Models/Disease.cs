using System.Text.Json.Serialization;

namespace Web.Models
{
    public class Disease 
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public string Description { get; set; }

        [JsonIgnore]
        public Department Department { get; set; }
    }
}
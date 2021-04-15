using System.Text.Json.Serialization;

namespace Web.Models
{
    public class Department 
    {
        public int Id { get; set; }
        public int HospitalId { get; set; }
        public string Description { get; set; }
    }
}
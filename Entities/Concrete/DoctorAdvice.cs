using System;
using System.Text.Json.Serialization;
using Core.Signatures;

namespace Entities.Concrete
{
    public class DoctorAdvice : IBaseEntity
    {
        public int Id { get; set; }
        public string Description { get; set; }
        
        public int DepartmentId { get; set; }
        
        [JsonIgnore]
        public string CreatedUserName { get; set; }
        [JsonIgnore]
        public DateTime CreatedAt { get; set; }

        [JsonIgnore]
        public Department Department { get; set; }
    }
}
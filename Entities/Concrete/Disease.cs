using System.Text.Json.Serialization;
using Core.Signatures;

namespace Entities.Concrete
{
    public class Disease : IBaseEntity
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; } = true;
        [JsonIgnore]
        public Department Department { get; set; }
    }
}
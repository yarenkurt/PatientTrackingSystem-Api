using Core.Signatures;
using Newtonsoft.Json;

namespace Entities.Concrete
{
    public class Doctor : IBaseEntity
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public int DegreeId { get; set; }
        public int PersonId { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }

        [JsonIgnore]
        public Department Department { get; set; }
        [JsonIgnore]
        public Person Person { get; set; }
        [JsonIgnore]
        public Degree Degree { get; set; }
    }
}
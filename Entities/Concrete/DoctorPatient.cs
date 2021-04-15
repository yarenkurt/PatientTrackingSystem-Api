using Core.Signatures;

namespace Entities.Concrete
{
    public class DoctorPatient : IBaseEntity
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }

        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
    }
}
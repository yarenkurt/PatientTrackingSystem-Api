using Entities.Concrete;

namespace Entities.Dtos
{
    public class GetDoctorDto
    {
        public string Email { get; set; }
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gsm { get; set; }
        
        public string DepartmentName { get; set; }
        public string DegreeName { get; set; }
    }
}
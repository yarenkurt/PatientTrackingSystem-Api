namespace Entities.Dtos
{
    public class InsertDoctorDto
    {
        public string Email { get; set; }
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gsm { get; set; }
       // public string Password { get; set; }
        public int DepartmentId { get; set; }
        public int DegreeId { get; set; }

    }
}
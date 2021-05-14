namespace Entities.Dtos
{
    public class UpdateDoctorDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gsm { get; set; }
        
        public int DepartmentId { get; set; }
        public int DegreeId { get; set; }
    }
}
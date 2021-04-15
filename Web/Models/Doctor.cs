namespace Web.Models
{
    public class Doctor 
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public int DegreeId { get; set; }
        public int PersonId { get; set; }
        public string Email { get; set; }
        public bool IsBlocked { get; set; }

        public Department Department { get; set; }
        public Person Person { get; set; }
        public Degree Degree { get; set; }
    }
}
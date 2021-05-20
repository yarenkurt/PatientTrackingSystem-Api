using System;
using System.Collections.Generic;

namespace Entities.Dtos
{
    public class GetPatientDto
    {
        public int Id { get; set; }
        public string IdentityNumber { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gsm { get; set; }
        public int Age { get; set; }
        public double Weight { get; set; }
        public int Height { get; set; }
        public decimal HealthScore { get; set; }
        public int Danger { get; set; }
        public int DepartmentId { get; set; }
        public int HospitalId { get; set; }
        public  List<string> Diseases{ get; set; }
        
    }
}
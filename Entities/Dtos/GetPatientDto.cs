using System.Collections.Generic;

namespace Entities.Dtos
{
    public class GetPatientDto
    {
        public string IdentityNumber { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gsm { get; set; }
        public decimal HealthScore { get; set; }

        public  List<string> Diseases{ get; set; }
        
    }
}
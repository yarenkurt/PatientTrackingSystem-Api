using System;

namespace Entities.Dtos
{
    public class InsertPatientDto
    {
        public string IdentityNumber { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gsm { get; set; }
        public int Age { get; set; }
        public double Weight { get; set; }
        public int Height { get; set; }

    }
    public class SOSDto
    {
        
        public double Latitude { get; set; }
        public double Longitude { get; set; }

    }
}
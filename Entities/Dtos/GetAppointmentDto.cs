using System;

namespace Entities.Dtos
{
    public class GetAppointmentDto
    {
        public int Id { get; set; }
        public string DoctorName { get; set; }
        public string PatientName { get; set; }
        public string DepartmentName { get; set; }
        public DateTime Date { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Api.Repositories;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class AppointmentsController : ControllerRepository<IAppointmentService, Appointment>
    {
        private readonly IAppointmentService _appointmentService;
        
        public AppointmentsController(IAppointmentService appointmentService) : base(appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet("Patients")]
        public async Task<IActionResult> GetByPatient([FromQuery, Required] int patientId)
        {
            return Ok(await _appointmentService.GetAllByPatient(patientId));
        }

        [HttpGet("Doctors")]
        public async Task<IActionResult> GetByDoctor([FromQuery, Required] int doctorId)
        {
            return Ok(await _appointmentService.GetAllByDoctor(doctorId));
        }

        [HttpGet("PatientDoctors")]
        public async Task<IActionResult> GetByPatientAndDoctor([FromQuery, Required] int patientId,
            [FromQuery, Required] int doctorId)
        {
            return Ok(await _appointmentService.GetAllByPatientAndDoctor(patientId, doctorId));
        }
    }
}
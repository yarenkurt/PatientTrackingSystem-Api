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

        [HttpGet("ByPatient")]
        public async Task<IActionResult> GetByPatient([FromQuery, Required] int patientId)
        {
            return Ok(await _appointmentService.GetAllActivesByPatient(patientId));
        }
        
        [HttpGet("ByPatientsExpired")]
        public async Task<IActionResult> GetExpiredByPatient([FromQuery, Required] int patientId)
        {
            return Ok(await _appointmentService.GetAllActivesByPatient(patientId));
        }

        [HttpGet("Doctors")]
        public async Task<IActionResult> GetByDoctor([FromQuery, Required] int doctorId)
        {
            return Ok(await _appointmentService.GetAllByDoctor(doctorId));
        }
        
        [HttpGet("Doctors/Expired")]
        public async Task<IActionResult> GetExpiredByDoctor([FromQuery, Required] int doctorId)
        {
            return Ok(await _appointmentService.GetAllExpiredByDoctor(doctorId));
        }

        [HttpGet("ByPatientDoctor")]
        public async Task<IActionResult> GetByDoctor([FromQuery, Required] int patientId,[FromQuery, Required] int doctorId)
        {
            return Ok(await _appointmentService.GetByPatientAndDoctor(patientId,doctorId));
        }

        [HttpGet("Closest")]
        public async Task<IActionResult> GetClosestAppointment([FromQuery, Required] int patientId)
        {
            return Ok(await _appointmentService.GetClosestAppointment(patientId));
        }

    }
}
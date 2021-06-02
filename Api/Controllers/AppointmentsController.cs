using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Api.Repositories;
using Business.Abstract;
using Core.Token;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class AppointmentsController : ControllerRepository<IAppointmentService, Appointment>
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IUserService _userService;
        public AppointmentsController(IAppointmentService appointmentService, IUserService userService) : base(appointmentService)
        {
            _appointmentService = appointmentService;
            _userService = userService;
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
        public async Task<IActionResult> GetClosestAppointment()
        {
            return Ok(await _appointmentService.GetClosestAppointment(_userService.PersonId));
        }

        
        [HttpGet("MyAppointments")]
        public async Task<IActionResult> GetMyAppointments()
        {
            return Ok(await _appointmentService.GetMyAppointments(_userService.PersonId));
        }
    }
}
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Api.Repositories;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class DoctorPatientsController : ControllerRepository<IDoctorPatientService, DoctorPatient>
    {
        private readonly IDoctorPatientService _doctorPatientService;
        
        public DoctorPatientsController(IDoctorPatientService doctorPatientService) : base(doctorPatientService)
        {
            _doctorPatientService = doctorPatientService;
        }

        [HttpGet("PatientsOfDoctor")]
        public async Task<IActionResult> GetPatientsOfDoctor([FromQuery, Required] int doctorId)
        {
            return Ok(await _doctorPatientService.GetPatientListOfDoctor(doctorId));
        }
        
        
        [HttpGet("DoctorsOfPatient")]
        public async Task<IActionResult> GetDoctorsOfPatient([FromQuery, Required] int patientId)
        {
            return Ok(await _doctorPatientService.GetDoctorListOfPatient(patientId));
        }
        
        
        [HttpGet("CountPatients")]
        public async Task<IActionResult> CountPatientsOfDoctor([FromQuery, Required] int doctorId)
        {
            return Ok(await _doctorPatientService.CountPatientOfDoctor(doctorId));
        }
    }
}
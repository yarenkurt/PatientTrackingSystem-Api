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

        [HttpGet("ActivePatientsOfDoctor")]
        public async Task<IActionResult> GetActivePatientsOfDoctor([FromQuery, Required] int doctorId)
        {
            return Ok(await _doctorPatientService.GetActivePatientListOfDoctor(doctorId));
        }
        
        [HttpGet("RemovedPatientsOfDoctor")]
        public async Task<IActionResult> GetPassivePatientsOfDoctor([FromQuery, Required] int doctorId)
        {
            return Ok(await _doctorPatientService.GetPassivePatientListOfDoctor(doctorId));
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
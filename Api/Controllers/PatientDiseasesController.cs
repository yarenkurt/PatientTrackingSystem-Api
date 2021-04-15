using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Api.Repositories;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class PatientDiseasesController : ControllerRepository<IPatientDiseaseService, PatientDisease>
    {
        private readonly IPatientDiseaseService _patientDiseaseService;
        
        public PatientDiseasesController(IPatientDiseaseService patientDiseaseService) : base(patientDiseaseService)
        {
            _patientDiseaseService = patientDiseaseService;
        }
        
        [HttpGet("ByPatient")]
        public async Task<IActionResult> GetByPatient([FromQuery, Required] int patientId)
        {
            return Ok(await _patientDiseaseService.GetAllByPatient(patientId));
        }
        
        [HttpGet("ByDept")]
        public async Task<IActionResult> GetByDept([FromQuery, Required] int deptId)
        {
            return Ok(await _patientDiseaseService.GetAllByDepartment(deptId));
        }
        
        [HttpGet]
        public async Task<IActionResult> GetByPatients()
        {
            return Ok(await _patientDiseaseService.GetAll());
        }

        [HttpPost("AddDisease")]
        public async Task<IActionResult> AddDisease([FromBody] PatientDisease newDisease)
        {
            return Ok(await _patientDiseaseService.AddDiseaseToPatient(newDisease));
        }
    }
}
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Api.Repositories;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class PatientRelativesController : ControllerRepository<IPatientRelativeService, PatientRelative>
    {
        private readonly IPatientRelativeService _patientRelativeService;
        
        public PatientRelativesController(IPatientRelativeService patientRelativeService) : base(patientRelativeService)
        {
            _patientRelativeService = patientRelativeService;
        }

        [HttpGet("Patients")]
        public async Task<IActionResult> GetAllRelatives([FromQuery, Required] int patientId)
        {
            return Ok(await _patientRelativeService.GetAllAsync(patientId));
        }
    }
}
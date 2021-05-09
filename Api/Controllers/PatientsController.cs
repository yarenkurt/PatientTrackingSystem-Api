using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientsController(IPatientService patientService)
        {
            _patientService = patientService;
        }
        
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _patientService.GetAllAsync());
        }
        
        [HttpGet("ById/{patientId}")]
        public async Task<IActionResult> GetById([FromRoute, Required] int patientId)
        {
            return Ok(await _patientService.GetAsync(patientId));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] InsertPatientDto dto)
        {
            return Ok(await _patientService.InsertAsync(dto));
        }
        
        
    }
}
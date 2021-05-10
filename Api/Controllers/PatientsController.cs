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
        
        [HttpPut]
        public async Task<IActionResult> Update([FromQuery] int patientId, [FromBody] InsertPatientDto patientDto)
        {
            return Ok(await _patientService.UpdateAsync(patientId,patientDto));
        }
        
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            return Ok(await _patientService.DeleteAsync(id));
        }
    }
}
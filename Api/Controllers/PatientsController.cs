using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Business;
using Business.Abstract;
using Core.Token;
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
        private readonly IUserService _userService;

        public PatientsController(IPatientService patientService, IUserService userService)
        {
            _patientService = patientService;
            _userService = userService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _patientService.GetAllAsync());
        }


        [HttpGet("Actives")]
        public async Task<IActionResult> GetAllActives()
        {
            return Ok(await _patientService.GetAllActivesAsync());
        }

        [HttpGet("Removed")]
        public async Task<IActionResult> GetAllRemoved()
        {
            return Ok(await _patientService.GetAllPassivesAsync());
        }

        [HttpGet("ById/{patientId}")]
        public async Task<IActionResult> GetById([FromRoute, Required] int patientId)
        {
            return Ok(await _patientService.GetAsync(patientId));
        }


        [HttpGet("MyProfile")]
        public async Task<IActionResult> MyProfile()
        {
            return Ok(await _patientService.GetByPersonIdAsync(_userService.PersonId));
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] InsertPatientDto dto)
        {
            return Ok(await _patientService.InsertAsync(dto));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromQuery] int patientId, [FromBody] InsertPatientDto patientDto)
        {
            return Ok(await _patientService.UpdateAsync(patientId, patientDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            return Ok(await _patientService.DeleteAsync(id));
        }

        [HttpGet("SOS")]
        public async Task<IActionResult> Post([FromQuery] double latitude, [FromQuery] double longitude)
        {
            return Ok(await _patientService.SOSAsync(_userService.PersonId, new SOSDto
            {
                Latitude = latitude,
                Longitude = longitude
            }));
        }
    }
}
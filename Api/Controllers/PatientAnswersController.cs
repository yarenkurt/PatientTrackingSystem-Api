using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Api.Repositories;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class PatientAnswersController : ControllerRepository<IPatientAnswerService, PatientAnswer>
    {
        private readonly IPatientAnswerService _patientAnswerService;
        
        public PatientAnswersController(IPatientAnswerService patientAnswerService) : base(patientAnswerService)
        {
            _patientAnswerService = patientAnswerService;
        }

        [HttpGet("TotalScoreOfPatient")]
        public IActionResult GetTotalScore([FromQuery, Required] int patientId)
        {
            return Ok( _patientAnswerService.GetTotalScoreOfPatient(patientId));
        }

        [HttpGet("AnswersOfPatient")]
        public async Task<IActionResult> GetAnswersOfPatient([FromQuery, Required] int patientId)
        {
            return Ok(await _patientAnswerService.GetAllAnswers(patientId));
        }
    }
}
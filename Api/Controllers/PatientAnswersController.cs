using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Api.Repositories;
using Business.Abstract;
using Core.Token;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class PatientAnswersController : ControllerRepository<IPatientAnswerService, PatientAnswer>
    {
        private readonly IPatientAnswerService _patientAnswerService;
        private readonly IUserService _userService;
        public PatientAnswersController(IPatientAnswerService patientAnswerService, IUserService userService) : base(patientAnswerService)
        {
            _patientAnswerService = patientAnswerService;
            _userService = userService;
        }

        [HttpGet("TotalScoreOfPatient")]
        public IActionResult GetTotalScore([FromQuery, Required] int patientId)
        {
            return Ok( _patientAnswerService.GetTotalScoreOfPatient(patientId));
        }
        [HttpGet("MyTotalScore")]
        public async Task<IActionResult> GetMyTotalScore()
        {
            var result = await _patientAnswerService.GetMyTotalScore(_userService.PersonId);
            return Ok( new {Score=result});
        }

        [HttpGet("AnswersOfPatient")]
        public async Task<IActionResult> GetAnswersOfPatient([FromQuery, Required] int patientId)
        {
            return Ok(await _patientAnswerService.GetAllAnswers(patientId));
        }
        
        [HttpGet("AnswerHistoryOfPatient")]
        public async Task<IActionResult> GetAnswerHistoryOfPatient([FromQuery, Required] int patientId)
        {
            return Ok(await _patientAnswerService.GetAnswerHistory(patientId));
        }
        [HttpPost("InsertPatientAnswer")]
        public async Task<IActionResult> InsertPatientAnswer([FromBody] InsertPatientAnswerDto entity)
        {
            var personId = _userService.PersonId;
            return Ok(await _patientAnswerService.InsertPatientAnswer(entity,personId));
        }
    }
}
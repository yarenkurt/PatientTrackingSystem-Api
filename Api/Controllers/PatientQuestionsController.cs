using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Api.Repositories;
using Business.Abstract;
using Core.Token;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class PatientQuestionsController : ControllerRepository<IPatientQuestionService,PatientQuestion>
    {
        private readonly IPatientQuestionService _patientQuestionService;
        private readonly IUserService _userService;
        public PatientQuestionsController(IPatientQuestionService patientQuestionService, IUserService userService) : base(patientQuestionService)
        {
            _patientQuestionService = patientQuestionService;
            _userService = userService;
        }

        [HttpGet("Questions")]
        public async Task<IActionResult> GetAll([FromQuery, Required] int patientId)
        {
            return Ok(await _patientQuestionService.GetAllQuestions(patientId));
        }

        [HttpPost("GetId")]
        public async Task<IActionResult> GetId([FromBody] PatientQuestion patQuestion)
        {
            return Ok(await _patientQuestionService.GetId(patQuestion));
        }
        
        [HttpGet("MyQuestions")]
        public async Task<IActionResult> MyQuestions()
        {
            return Ok(await _patientQuestionService.MyQuestions(_userService.PersonId));
        }
        [HttpGet("Questions/{id:int}")]
        public async Task<IActionResult> Questions([FromRoute]int id)
        {
            return Ok(await _patientQuestionService.Questions(id,_userService.PersonId));
        }
    }
}
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Api.Repositories;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class PatientQuestionsController : ControllerRepository<IPatientQuestionService,PatientQuestion>
    {
        private readonly IPatientQuestionService _patientQuestionService;
        
        public PatientQuestionsController(IPatientQuestionService patientQuestionService) : base(patientQuestionService)
        {
            _patientQuestionService = patientQuestionService;
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
    }
}
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Api.Repositories;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class AnswersController : ControllerRepository<IAnswerPoolService,AnswerPool>
    {
        private readonly IAnswerPoolService _answerService;
        
        public AnswersController(IAnswerPoolService answerService) : base(answerService)
        {
            _answerService = answerService;
        }

        [HttpGet("Answers")]
        public async Task<IActionResult> GetAll([FromQuery, Required] int questionId)
        {
            return Ok(await _answerService.GetAnswersOfQuestion(questionId));
        }
        
        
    }
}
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Api.Repositories;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class QuestionsController : ControllerRepository<IQuestionPoolService,QuestionPool>
    {
        private readonly IQuestionPoolService _questionPoolService;
        
        public QuestionsController(IQuestionPoolService questionPoolService) : base(questionPoolService)
        {
            _questionPoolService = questionPoolService;
        }


        [HttpGet("All")]
        public async Task<IActionResult> GetAll([FromQuery, Required] int deptId)
        {
            return Ok(await _questionPoolService.GetAllByDept(deptId));
        }
        
        
        [HttpGet("GetNumeric")]
        public async Task<IActionResult> GetAllNumeric()
        {
            return Ok(await _questionPoolService.GetOnlyNumQ());
        }
        
        [HttpGet("GetMultipleChoices")]
        public async Task<IActionResult> GetAllMultipleChoices()
        {
            return Ok(await _questionPoolService.GetOnlyMCQ());
        }
        
        
    }
}
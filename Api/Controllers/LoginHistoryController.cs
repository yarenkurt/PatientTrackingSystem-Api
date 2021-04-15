using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Api.Repositories;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class LoginHistoryController : ControllerRepository<IPersonLoginHistoryService, PersonLoginHistory>
    {
        private readonly IPersonLoginHistoryService _loginHistoryService;
        
        public LoginHistoryController(IPersonLoginHistoryService loginHistoryService) : base(loginHistoryService)
        {
            _loginHistoryService = loginHistoryService;
        }

        [HttpGet("Patient")]
        public async Task<IActionResult> GetPersonHistory([FromQuery, Required] int personId)
        {
            return Ok(await _loginHistoryService.GetPersonHistory(personId));
        }
    }
}
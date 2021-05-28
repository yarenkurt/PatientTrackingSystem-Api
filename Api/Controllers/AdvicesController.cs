using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Api.Repositories;
using Business.Abstract;
using Core.Token;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class AdvicesController : ControllerRepository<IAdviceService, DoctorAdvice>
    {
        private readonly IAdviceService _adviceService;
        private readonly IUserService _userService;

        public AdvicesController(IAdviceService adviceService, IUserService userService) : base(adviceService)
        {
            _adviceService = adviceService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery, Required] int deptId)
        {
            return Ok(await _adviceService.GetAllByDept(deptId));
        }

        [HttpGet("MyAdvices")]
        public async Task<IActionResult> GetMyAdvices()
        {
            return Ok(await _adviceService.GetMyAdvices(_userService.PersonId));
        }
    }
}
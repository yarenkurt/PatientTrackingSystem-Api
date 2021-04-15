using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Api.Repositories;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class AdvicesController : ControllerRepository<IAdviceService, DoctorAdvice>
    {
        private readonly IAdviceService _adviceService;
        
        public AdvicesController(IAdviceService adviceService) : base(adviceService)
        {
            _adviceService = adviceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery, Required] int deptId)
        {
            return Ok(await _adviceService.GetAllByDept(deptId));
        }
    }
}
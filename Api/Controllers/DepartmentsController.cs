using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Api.Repositories;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class DepartmentsController : ControllerRepository<IDepartmentService,Department>
    {
        private readonly IDepartmentService _departmentService;
        
        public DepartmentsController(IDepartmentService departmentService) : base(departmentService)
        {
            _departmentService = departmentService;
        }

  
        [HttpGet("Count")]
        public async Task<IActionResult> Count([FromQuery, Required] int hospitalId)
        {
            return Ok(await _departmentService.CountAsync(hospitalId));
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery, Required] int hospitalId)
        {
            return Ok(await _departmentService.GetAllAsync(hospitalId));
        }
    }
}
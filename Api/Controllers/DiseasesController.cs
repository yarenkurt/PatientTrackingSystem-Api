using System.Threading.Tasks;
using Api.Repositories;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class DiseasesController : ControllerRepository<IDiseaseService,Disease>
    {
        private readonly IDiseaseService _diseaseService;
        
        public DiseasesController(IDiseaseService diseaseService) : base(diseaseService)
        {
            _diseaseService = diseaseService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _diseaseService.GetAllAsync());
        }
        
        [HttpGet("ByDepartment")]
        public async Task<IActionResult> GetAllByDept([FromQuery] int deptId)
        {
            return Ok(await _diseaseService.GetAllAsyncByDept(deptId));
        }
    }
}
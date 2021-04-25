using System.Threading.Tasks;
using Api.Repositories;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class DegreesController : ControllerRepository<IDegreeService, Degree>
    {
        private readonly IDegreeService _degreeService;
        public DegreesController(IDegreeService degreeService) : base(degreeService)
        {
            _degreeService = degreeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _degreeService.GetAll());
        }
    }
}
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Api.Repositories;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class CitiesController : ControllerRepository<ICityService,City>
    {
        private readonly ICityService _cityService;
        
        public CitiesController(ICityService service) : base(service)
        {
            _cityService = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery,Required] int countryId)
        {
            return Ok(await _cityService.GetAllAsync(countryId));
        }
        
        [HttpGet("All")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _cityService.GetAll());
        }

        [HttpGet("Search")]
        public async Task<IActionResult> Search([FromQuery] string searchKey)
        {
            return Ok(await _cityService.SearchCity(searchKey));
        }

        [HttpGet("Count")]
        public async Task<IActionResult> Count()
        {
            return Ok(await _cityService.CountAsync());
        }
        
    }
}
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Api.Repositories;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Authorize(Roles = "3")]
    public class DistrictsController : ControllerRepository<IDistrictService,District>
    {
        private readonly IDistrictService _districtService;
        
        public DistrictsController(IDistrictService districtService) : base(districtService)
        {
            _districtService = districtService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery, Required] int cityId)
        {
            return Ok(await _districtService.GetAllAsync(cityId));
        }
        
        [HttpGet("Count")]
        public async Task<IActionResult> Count()
        {
            return Ok(await _districtService.CountAsync());
        }
        
        [HttpGet("All")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _districtService.GetAllDistricts());
        }
    }
}
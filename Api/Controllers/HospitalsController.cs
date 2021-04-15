using System.Threading.Tasks;
using Api.Repositories;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class HospitalsController : ControllerRepository<IHospitalService, Hospital>
    {
        private readonly IHospitalService _hospitalService;
        
        public HospitalsController(IHospitalService hospitalService) : base(hospitalService)
        {
            _hospitalService = hospitalService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int districtId)
        {
            return Ok(await _hospitalService.GetAllAsync(districtId));
        }
    }
}
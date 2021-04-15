using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminsController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminsController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _adminService.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] InsertAdminDto dto)
        {
            return Ok(await _adminService.InsertAsync(dto));
        }
    }
}
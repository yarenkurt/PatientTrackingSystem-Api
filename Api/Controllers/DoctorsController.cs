using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Api.Repositories;
using Business.Abstract;
using Core.Enums;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        
        public DoctorsController(IDoctorService doctorService) 
        {
            _doctorService = doctorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _doctorService.GetAllAsync());
        }
        
        [HttpGet("ByDepartment")]
        public async Task<IActionResult> GetAllByDept([FromQuery, Required] int deptId)
        {
            return Ok(await _doctorService.GetAllByDeptAsync(deptId));
        }

        [HttpGet("ByDegree")]
        public async Task<IActionResult> GetAllByDegree([FromQuery, Required] int degreeId)
        {
            return Ok(await _doctorService.GetAllByDegreeAsync(degreeId));
        }

        [HttpPost]
        public async Task<IActionResult> InsertAsync(InsertDoctorDto dto)
        {
            return Ok(await _doctorService.InsertAsync(dto));
        }
        
        [HttpGet("Count")]
        public async Task<IActionResult> Count([FromQuery, Required] int hospitalId)
        {
            return Ok(await _doctorService.CountAsync(hospitalId));
        }

        [HttpGet("ByHospital")]
        public async Task<IActionResult> GetAllByHospital([FromQuery, Required] int hospitalId)
        {
            return Ok(await _doctorService.GetAllByHospitalAsync(hospitalId));
        }
    }
}
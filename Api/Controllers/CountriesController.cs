using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Repositories;
using Business.Abstract;
using Core.Enums;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class CountriesController : ControllerRepository<ICountryService,Country>
    {
        private readonly ICountryService _countryService;
        
        public CountriesController(ICountryService countryService) : base(countryService)
        {
            _countryService = countryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _countryService.GetAllAsync());
        }


        [HttpGet("SelectList")]
        public async Task<IActionResult> SelectList()
        {
            return Ok(await _countryService.SelectListAsync());
        }

        [HttpGet("Count")]
        public async Task<IActionResult> Count()
        {
            return Ok(await _countryService.CountAsync());
        }
    }
}
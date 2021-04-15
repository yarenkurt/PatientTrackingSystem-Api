using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authService;

        public AuthenticationController(IAuthenticationService authService)
        {
            _authService = authService;
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            return Ok(await _authService.LoginAsync(loginDto));
        }


        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> PasswordChangeRequest([FromQuery, Required] string gsm)
        {
            return Ok(await _authService.ForgotPassword(gsm));
        }
    }
}
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Enums;
using Core.Helpers;
using Core.Models;
using Core.Token;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authService;
        private readonly IUserService _userService;
        
        public AuthenticationController(IAuthenticationService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            return Ok(await _authService.LoginAsync(loginDto));
        }

        [HttpGet("UserInfo")]
        public async Task<IActionResult> UserInfo()
        {
            return await Task.Run(() => Ok(_userService.UserInfo));
        }

        [HttpGet("PersonTypes")]
        public async Task<IActionResult> PersonTypes()
        {
            return await Task.Run(() => Ok(EnumHelper.List<PersonType>().ToList().Where(x => x.Id > 0).ToList()));
        }
        
        
        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> PasswordChangeRequest([FromQuery, Required] string gsm)
        {
            return Ok(await _authService.ForgotPassword(gsm));
        }
        

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout([FromBody, Required] int userId)
        {
            return Ok(await _authService.LogoutAsync(userId));
        }

        [HttpPost("RefreshLogin")]
        public async Task<IActionResult> RefreshAsync([FromBody, Required] string refreshToken)
        {
            return Ok(await _authService.RefreshAsync(refreshToken));
        }
    }
}
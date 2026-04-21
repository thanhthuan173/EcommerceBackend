using AutoMapper;
using BeverageBackend.Dto;
using BeverageBackend.Dto.Auth;
using BeverageBackend.Interfaces;
using BeverageBackend.Interfaces.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace BeverageBackend.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class AccountController : Controller
    {
        private readonly IAccountService _service;

        public AccountController(IAccountService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _service.Register(dto);
            return Ok("Regiter successfully");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var token = await _service.Login(dto);
                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(RefreshTokenDto dto)
        {
            try
            {
                var token = await _service.RefreshToken(dto);
                return Ok(token);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout(RefreshTokenDto dto)
        {
            await _service.Logout(dto);
            return Ok();
        }
    }
}

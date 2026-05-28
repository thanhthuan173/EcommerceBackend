using AutoMapper;
using BeverageBackend.Application.Dto.Auth;
using BeverageBackend.Application.Interfaces.Services;
using BeverageBackend.Application.Dto;
using BeverageBackend.Application.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace BeverageBackend.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _service;

        public AccountController(IAccountService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            await _service.Register(dto);
            return Created();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            return Ok(await _service.Login(dto));
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDto dto)
        {
            return Ok(await _service.RefreshToken(dto));
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] RefreshTokenDto dto)
        {
            await _service.Logout(dto);
            return NoContent();
        }
    }
}

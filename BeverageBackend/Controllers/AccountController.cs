using AutoMapper;
using BeverageBackend.Dto;
using BeverageBackend.Dto.Auth;
using BeverageBackend.Interfaces;
using BeverageBackend.Interfaces.Services;
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

        [HttpPost("/register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _service.Register(dto);
            return Ok("Regiter successfully");
        }
    }
}

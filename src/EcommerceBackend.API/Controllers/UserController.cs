using AutoMapper;
using EcommerceBackend.Application.Common.Query;
using EcommerceBackend.Application.Dto;
using EcommerceBackend.Application.Dto.User;
using EcommerceBackend.Application.Interfaces;
using EcommerceBackend.Application.Interfaces.Services;
using EcommerceBackend.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceBackend.API.Controllers
{
    [Authorize]
    [Route("api/[Controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            return Ok(await _service.GetProfileAsync());
        }

        [HttpPatch("profile")]
        public async Task<IActionResult> UpdateProfile(UpdateProfileDto dto)
        {
            await _service.UpdateProfileAsync(dto);
            return NoContent();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetUser(int id)
        {
            return Ok(await _service.GetUserByIdAsync(id));
        }

        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetAllUsers([FromQuery] UserQueryParameters query)
        {
            var result = await _service.GetUsersAsync(query);
            return Ok(new
            {
                result.Items,
                result.TotalCount,
                result.PageNumber,
                result.PageSize,
                result.TotalPages
            });
        }

        [HttpPatch("{id}/activate")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Activate(int id)
        {
            await _service.ActivateUserAsync(id);
            return NoContent();
        }

        [HttpPatch("{id}/deactivate")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Deactivate(int id)
        {
            await _service.DeactivateUserAsync(id);
            return NoContent();
        }
    }
}

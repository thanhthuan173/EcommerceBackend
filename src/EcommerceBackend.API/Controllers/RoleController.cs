using EcommerceBackend.Application.Dto.Role;
using EcommerceBackend.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceBackend.API.Controllers
{
    [Authorize(Roles ="ADMIN")]
    [ApiController]
    [Route("api/[Controller]")]
    public class RoleController : Controller
    {
        private readonly IRoleService _service;

        public RoleController(IRoleService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _service.GetRolesAsync();
            return Ok(roles);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RoleDto dto)
        {
            await _service.CreateAsync(dto.Name);
            return Ok("Created successfully");
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var role = await _service.GetByNameAsync(name);
            if (role == null)
            {
                return NotFound("Role not found");
            }
            return Ok(role);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var role = await _service.GetByIdAsync(id);
            if (role == null)
            {
                return NotFound("Role not found");
            }
            return Ok(role);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateName(int id, [FromBody]RoleDto dto)
        {
            await _service.UpdateNameAsync(id, dto.Name);
            return Ok("Update successfully");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}

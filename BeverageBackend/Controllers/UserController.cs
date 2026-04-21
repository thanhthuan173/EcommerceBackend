using AutoMapper;
using BeverageBackend.Dto;
using BeverageBackend.Interfaces;
using BeverageBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BeverageBackend.Controllers
{
    [Authorize]
    [Route("api/[Controller]")]
    [ApiController]
    public class UserController:ControllerBase
    {
        private IUserRepository _user;
        private IMapper _mapper;

        public UserController(IUserRepository user,IMapper mapper)
        {
            _user = user;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllUser()
        {
            var users= _mapper.Map<List<UserDto>>(_user.GetUsers());
            return Ok(users);
        }
            

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            if (!_user.UserExits(id))
                return NotFound();
            var cus = _mapper.Map<UserDto>(_user.GetUser(id));
            return Ok(cus);
        }

        [HttpGet("{id}/orders")]
        public IActionResult GetOrders(int id)
        {
            if (_user.UserExits(id))
                return NotFound();
            var orders = _mapper.Map<List<OrderDto>>(_user.GetOrdersByUser(id));
            return Ok(orders);
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] CreateUserDto createUserDto)
        {
            var isExist = _user.GetUsers().Where(c => c.Id == createUserDto.Id).FirstOrDefault();
            if (_user.UserExits(createUserDto.Id))
            {
                ModelState.AddModelError("User", "User already exist");
                return BadRequest(ModelState);
            }
            var userMap = _mapper.Map<User>(createUserDto);
            if (!_user.CreateUser(userMap))
            {
                return StatusCode(500, "Error while saving");
            }
            return Ok("Create successfully");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            if (!_user.UserExits(id))
            {
                return NotFound();
            }
            if (!_user.DeleteUser(id))
            {
                return StatusCode(500, "Error while saving");
            }
            return Ok("Delete successfully");
        }
    }
}

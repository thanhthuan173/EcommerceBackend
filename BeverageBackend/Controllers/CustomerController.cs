using AutoMapper;
using BeverageBackend.Dto;
using BeverageBackend.Interfaces;
using BeverageBackend.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BeverageBackend.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class CustomerController:ControllerBase
    {
        private ICustomerRepository _customer;
        private IMapper _mapper;

        public CustomerController(ICustomerRepository customer,IMapper mapper)
        {
            _customer = customer;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllCustomer()
        {
            var customers= _mapper.Map<List<CustomerDto>>(_customer.GetCustomers());
            return Ok(customers);
        }
            

        [HttpGet("{id}")]
        public IActionResult GetCustomer(int id)
        {
            if (!_customer.CustomerExits(id))
                return NotFound();
            var cus = _mapper.Map<CustomerDto>(_customer.GetCustomer(id));
            return Ok(cus);
        }

        [HttpGet("{id}/orders")]
        public IActionResult GetOrders(int id)
        {
            if (_customer.CustomerExits(id))
                return NotFound();
            var orders = _mapper.Map<List<OrderDto>>(_customer.GetOrdersByCustomer(id));
            return Ok(orders);
        }

        [HttpPost]
        public IActionResult CreateCustomer([FromBody] CreateCustomerDto createCustomerDto)
        {
            var isExist = _customer.GetCustomers().Where(c => c.Id == createCustomerDto.Id).FirstOrDefault();
            if (_customer.CustomerExits(createCustomerDto.Id))
            {
                ModelState.AddModelError("Customer","Customer already exist");
                return BadRequest(ModelState);
            }
            var customerMap = _mapper.Map<Customer>(createCustomerDto);
            if (!_customer.CreateCustomer(customerMap))
            {
                return StatusCode(500, "Error while saving");
            }
            return Ok("Create successfully");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            if (!_customer.CustomerExits(id))
            {
                return NotFound();
            }
            if (!_customer.DeleteCustomer(id))
            {
                return StatusCode(500, "Error while saving");
            }
            return Ok("Delete successfully");
        }
    }
}

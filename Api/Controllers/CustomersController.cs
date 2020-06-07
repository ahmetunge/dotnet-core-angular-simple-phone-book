using System.Threading.Tasks;
using Api.Entities;
using Api.Entities.Helpers;
using Api.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;

        }



        [HttpGet("{id}", Name = "GetCustomer")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            return Ok(await _customerService.GetCustomerById(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers([FromQuery] CustomerParams customerParams)
        {
            return Ok(await _customerService.FindCustomersAsync(customerParams));
        }


        [HttpPost]
        public async Task<IActionResult> AddCustomers([FromBody] Customer customer)
        {
            var createdCustomer = await _customerService.AddCustomerAsync(customer);

            if (createdCustomer != null)
            {
                return CreatedAtRoute("GetCustomer", new { id = customer.Id }, createdCustomer);
            }

            return BadRequest("Customer could not created");

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, Customer customer)
        {
            if (await _customerService.UpdateCustomer(id, customer))
                return NoContent();


            return BadRequest("Customer could not updated");
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            if (await _customerService.DeleteCustomer(id))
                return NoContent();


            return BadRequest("Customer could not deleted");
        }
    }
}
using Microsoft.AspNetCore.Mvc;
namespace CustomerApp;

[ApiController]
[Route("api/v1/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    
    // GET: api/v1/customer
    [HttpGet]
    public async Task<IActionResult> GetAllCustomers()
    {
        var customers = await _customerService.GetAllCustomersAsync();
        return Ok(customers);
    }

    // GET: api/v1/customer/1
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetCustomerById(int id)
    {
        var customer = await _customerService.GetCustomerByIdAsync(id);
        if (customer == null) return NotFound();

        return Ok(customer);
    }

    // POST: api/v1/customer
    [HttpPost]
    public async Task<IActionResult> CreateCustomer([FromBody] CustomerDTO dto)
    {
        var created = await _customerService.CreateCustomerAsync(dto);

        if (created == null)
            return BadRequest();

        return CreatedAtAction(nameof(GetCustomerById),
                               new { id = created.Id },
                               created);
    }


    // PUT: api/v1/customer/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCustomer(int id, [FromBody] CustomerDTO dto)
    {
        var updated = await _customerService.UpdateCustomerAsync(id, dto);
        if (updated == null) return NotFound();

        return Ok(updated);
    }

    // DELETE: api/v1/customer/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer(int id)
    {
        var deleted = await _customerService.DeleteCustomerAsync(id);
        if (!deleted) return NotFound();

        return NoContent();
    }
}

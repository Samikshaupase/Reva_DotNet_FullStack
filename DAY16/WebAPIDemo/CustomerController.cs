
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private static List<Customer> customers = new List<Customer>
    {
        new Customer { Id = 1, FirstName = "John", LastName = "Doe", Email = "John.Doe@gmail.com", Source = "Website", Status = "New", Budget = 1000 },
        new Customer { Id = 2, FirstName = "Jane", LastName = "Smith", Email = "Jame.Smith@gmail.com", Source = "Referral", Status = "Contacted", Budget = 2000 }
    };

    [HttpGet()]
    public ActionResult<List<Customer>> GetAllCustomers()
    {
        Console.WriteLine($"Total Customers: {customers.Count}");
        return Ok(customers);
        // return BadRequest();
    }

    // GET BY ID
    [HttpGet("{id}")]
    public ActionResult<Customer> GetById(int id)
    {
        var customer = customers.FirstOrDefault(c => c.Id == id);
        if (customer == null)
            return NotFound();

        return Ok(customer);
    }

    // POST
    [HttpPost]
    public ActionResult<Customer> Create(Customer customer)
    {
        customer.Id = customers.Max(c => c.Id) + 1;
        customers.Add(customer);
        return CreatedAtAction(nameof(GetById), new { id = customer.Id }, customer);
    }

    // PUT
    [HttpPut("{id}")]
    public IActionResult Update(int id, Customer updatedCustomer)
    {
        var customer = customers.FirstOrDefault(c => c.Id == id);
        if (customer == null)
            return NotFound();

        customer.FirstName = updatedCustomer.FirstName;
        customer.LastName = updatedCustomer.LastName;
        customer.Email = updatedCustomer.Email;

        return NoContent();
    }

    // PATCH
    [HttpPatch("{id}")]
    public IActionResult Patch(int id, Customer updatedCustomer)
    {
        var customer = customers.FirstOrDefault(c => c.Id == id);
        if (customer == null)
            return NotFound();

        if (!string.IsNullOrEmpty(updatedCustomer.FirstName))
            customer.FirstName = updatedCustomer.FirstName;

        if (!string.IsNullOrEmpty(updatedCustomer.Email))
            customer.Email = updatedCustomer.Email;

        return NoContent();
    }

     // DELETE
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var customer = customers.FirstOrDefault(c => c.Id == id);
        if (customer == null)
            return NotFound();

        customers.Remove(customer);
        return NoContent();
    }

}
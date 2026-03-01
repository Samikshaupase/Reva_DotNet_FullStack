namespace CustomerApp;

public class CustomerDTO
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public string? Email { get; set; }

}

public interface ICustomerService
{
    Task<List<CustomerDTO>> GetAllCustomersAsync();
    Task<CustomerDTO?> GetCustomerByIdAsync(int id);
    Task<CustomerDTO?> CreateCustomerAsync(CustomerDTO dto);

    Task<CustomerDTO?> UpdateCustomerAsync(int id, CustomerDTO dto);

    Task<bool> DeleteCustomerAsync(int id);
}

// Services/CustomerService.cs

public class CustomerService : ICustomerService
{
    private readonly ILogger<CustomerService> _logger;

    // In-memory storage for practice
    private static List<CustomerDTO> _customers = new List<CustomerDTO>
    {
        new CustomerDTO { Id = 1, Name = "Acme Corp", Email = "contact@acme.com" },
        new CustomerDTO { Id = 2, Name = "TechStart Inc", Email = "info@techstart.com" }
    };

    public CustomerService(ILogger<CustomerService> logger)
    {
        _logger = logger;
    }

    public Task<List<CustomerDTO>> GetAllCustomersAsync()
    {
        return Task.FromResult(_customers);
    }

    public Task<CustomerDTO?> GetCustomerByIdAsync(int id)
    {
        var customer = _customers.FirstOrDefault(c => c.Id == id);
        return Task.FromResult(customer);
    }


    public Task<CustomerDTO?> CreateCustomerAsync(CustomerDTO dto)
    {
        dto.Id = _customers.Max(c => c.Id) + 1;
        _customers.Add(dto);
        return Task.FromResult<CustomerDTO?>(dto);
    }


    public Task<CustomerDTO?> UpdateCustomerAsync(int id, CustomerDTO dto)
    {
        var customer = _customers.FirstOrDefault(c => c.Id == id);
        if (customer == null) return Task.FromResult<CustomerDTO?>(null);

        customer.Name = dto.Name;
        customer.Email = dto.Email;

        return Task.FromResult<CustomerDTO?>(customer);
    }

    public Task<bool> DeleteCustomerAsync(int id)
    {
        var customer = _customers.FirstOrDefault(c => c.Id == id);
        if (customer == null) return Task.FromResult(false);

        _customers.Remove(customer);
        return Task.FromResult(true);
    }
}
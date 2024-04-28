// Abstract service definition
public interface ICustomerRepository
{
    void Add(Customer customer);
}

// High-level business logic module
public class CustomerService
{
    private ICustomerRepository _customerRepository;
    public CustomerService(ICustomerRepository customerData)
    {
        _customerRepository = customerData;
    }

    public void RegisterCustomer(Customer customer)
    {
        _customerRepository.Add(customer);
    }
}

// Low-level data access module
public class SqlCustomerRepository : ICustomerRepository
{
    public void Add(Customer customer) { /* Code to add customer to a SQL database */ }
}

// Model
public record class Customer
{
    public int Id { get; init; }
    public required string Name { get; init; }
}

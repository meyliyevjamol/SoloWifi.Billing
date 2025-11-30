using SoloWifi.Billing.DataLayer;

namespace SoloWifi.Billing.ServiceLayer;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public Task<List<Customer>> GetAllAsync()
    {
        return _customerRepository.GetAllAsync();   
    }
}

namespace SoloWifi.Billing.DataLayer;

public interface ICustomerRepository
{
    Task<List<Customer>> GetAllAsync(long id);
    Task<Customer> GetByIdAsync(long id);
    Task<Customer> CreateAsync(Customer customer);
    Task<Customer> UpdateAsync(Customer customer);
    Task<long> RemoveAsync(Customer customer);
}

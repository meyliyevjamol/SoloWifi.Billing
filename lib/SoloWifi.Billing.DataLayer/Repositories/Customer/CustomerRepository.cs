
using Microsoft.EntityFrameworkCore;

namespace SoloWifi.Billing.DataLayer;

public class CustomerRepository : ICustomerRepository
{
    private readonly EfCoreContext _context;

    public CustomerRepository(EfCoreContext context)
    {
        _context = context;
    }

    public async Task<Customer> CreateAsync(Customer customer)
    {
         await _context.Customers.AddAsync(customer);
        _context.SaveChanges();
        return customer;
    }

    public Task<List<Customer>> GetAllAsync(long id)
    {
        throw new NotImplementedException();
    }

    public async Task<Customer> GetByIdAsync(long id)
    {
        return await _context.Customers.FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<long> RemoveAsync(Customer customer)
    {
        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();
        return customer.Id;
    }

    public async Task<Customer> UpdateAsync(Customer customer)
    {
         _context.Customers.Update(customer);
        await _context.SaveChangesAsync();
        return customer;
    }
}

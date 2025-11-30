using SoloWifi.Billing.DataLayer;

namespace SoloWifi.Billing.ServiceLayer;

public interface ICustomerService
{
    Task<List<Customer>> GetAllAsync();
}

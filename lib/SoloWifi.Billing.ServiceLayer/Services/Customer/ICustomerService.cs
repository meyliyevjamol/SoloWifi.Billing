using SoloWifi.Billing.DataLayer;

namespace SoloWifi.Billing.ServiceLayer.Services;

public interface ICustomerService
{
    Task<List<Customer>> GetAllAsync();
}

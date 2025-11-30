using SoloWifi.Billing.DataLayer;

namespace SoloWifi.Billing.ServiceLayer;

public interface IPackageService
{
    Task<List<Package>> GetAllAsync();
}

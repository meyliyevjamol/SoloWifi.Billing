namespace SoloWifi.Billing.DataLayer;

public interface IPackageRepository
{
    Task<Package> GetByIdAsync(long id);
    Task<List<Package>> GetAllAsync();
}

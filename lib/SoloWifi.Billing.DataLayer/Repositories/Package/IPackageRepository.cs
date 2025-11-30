namespace SoloWifi.Billing.DataLayer;

public interface IPackageRepository
{
    Task<Package> GetByIdAsync(long id);
}

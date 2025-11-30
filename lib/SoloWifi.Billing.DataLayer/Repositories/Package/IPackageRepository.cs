namespace SoloWifi.Billing.DataLayer.Repositories;

public interface IPackageRepository
{
    Task<Package> GetByIdAsync(long id);
}

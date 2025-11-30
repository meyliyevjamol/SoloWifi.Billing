using SoloWifi.Billing.DataLayer;

namespace SoloWifi.Billing.ServiceLayer;

public class PackageService : IPackageService
{
    private readonly IPackageRepository _PackageRepository;

    public PackageService(IPackageRepository PackageRepository)
    {
        _PackageRepository = PackageRepository;
    }

    public Task<List<Package>> GetAllAsync()
    {
        return _PackageRepository.GetAllAsync();
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoloWifi.Billing.DataLayer;

public class PackageRepository : IPackageRepository
{
    private readonly EfCoreContext _context;

    public PackageRepository(EfCoreContext context)
    {
        _context = context;
    }

    public async Task<Package> GetByIdAsync(long id)
    {
        return await _context.Packages.FindAsync(id);
    }
    public async Task<List<Package>> GetAllAsync()
    {
        return await _context.Packages.ToListAsync();
    }
}

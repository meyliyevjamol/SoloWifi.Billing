using SoloWifi.Billing.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoloWifi.Billing.ServiceLayer;

public interface IPackageService
{
    Task<List<Package>> GetAllAsync();
}

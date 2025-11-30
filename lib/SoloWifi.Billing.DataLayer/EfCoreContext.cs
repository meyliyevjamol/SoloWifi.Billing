using Microsoft.EntityFrameworkCore;

namespace SoloWifi.Billing.DataLayer;

public class EfCoreContext : DbContext
{
    public EfCoreContext()
    {
    }

    public EfCoreContext(DbContextOptions<EfCoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Package> Packages { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

}


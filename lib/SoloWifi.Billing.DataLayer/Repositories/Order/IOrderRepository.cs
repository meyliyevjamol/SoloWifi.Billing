using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoloWifi.Billing.DataLayer;

public interface IOrderRepository
{
    Task<Order> GetByIdAsync(int id);
    Task<List<Order>> GetAllAsync();
    Task<Order> GetByIdWithDetailsAsync(int id);
    Task<Order> CreateAsync(Order order);
    Task UpdateAsync(Order order);
}

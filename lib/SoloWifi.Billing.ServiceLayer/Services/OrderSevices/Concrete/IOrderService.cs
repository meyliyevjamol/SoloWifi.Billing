using SoloWifi.Billing.DataLayer;
using StatusGeneric;

namespace SoloWifi.Billing.ServiceLayer;

public interface IOrderService : IStatusGeneric
{
    Task<Order> CreateOrderAsync(CreateOrderModel request);
    Task<List<Order>> GetAllAsync();
    Task<Order> PayOrderAsync(int orderId);
}

using SoloWifi.Billing.DataLayer;
using StatusGeneric;

namespace SoloWifi.Billing.ServiceLayer.Services;

public interface IOrderService : IStatusGeneric
{
    Task<Order> CreateOrderAsync(CreateOrderModel request);
    Task<Order> PayOrderAsync(int orderId);
}

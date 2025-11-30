using Confluent.Kafka;
using Microsoft.EntityFrameworkCore;
using SoloWifi.Billing.DataLayer;
using StatusGeneric;
using System.Text.Json;

namespace SoloWifi.Billing.ServiceLayer;

public class OrderService : StatusGenericHandler, IOrderService
{
    private readonly EfCoreContext _context;
    private readonly IProducer<Null, string> _producer;
    private readonly IOrderRepository _orderRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IPackageRepository _packageRepository;

    public OrderService(
        EfCoreContext context, IProducer<Null, string> producer, IOrderRepository orderRepository, ICustomerRepository customerRepository, IPackageRepository packageRepository)
    {
        _context = context;
        _producer = producer;
        _orderRepository = orderRepository;
        _customerRepository = customerRepository;
        _packageRepository = packageRepository;
    }

    public async Task<Order> CreateOrderAsync(CreateOrderModel request)
    {
        var customer = await _customerRepository.GetByIdAsync(request.CustomerId);
        if (customer == null)
            throw new InvalidOperationException("Mijoz topilmadi");

        var package = await _packageRepository.GetByIdAsync(request.PackageId);
        if (package == null)
            throw new InvalidOperationException("Paket topilmadi");

        var order = new Order
        {
            CustomerId = request.CustomerId,
            PackageId = request.PackageId,
            StatusId = 1,
            CreatedAt = DateTime.Now//yaratilgan
        };

        await _orderRepository.CreateAsync(order);
        await _context.SaveChangesAsync();

        return order;
    }

    public async Task<Order> PayOrderAsync(int orderId)
    {
        var order = await _context.Orders
            .Include(o => o.Customer)
            .Include(o => o.Package)
            .FirstOrDefaultAsync(o => o.Id == orderId);

        if (order == null)
            throw new InvalidOperationException("Buyurtma topilmadi");

        if (order.StatusId != StatusIdConst.CREATED)
            throw new InvalidOperationException("Buyurtma allaqachon to'langan yoki bekor qilingan");

        if (order.Customer == null || order.Package == null)
            throw new InvalidOperationException("Buyurtma ma'lumotlari noto'liq");

        // Balans yetarliligini tekshirish
        if (order.Customer.Balance < order.Package.Price)
        {
            order.StatusId = StatusIdConst.FAILED;
            await _context.SaveChangesAsync();
            throw new InvalidOperationException("Balans yetarli emas");
        }

        // To'lovni amalga oshirish - Balansdan avtomatik yechib olish
        order.Customer.Balance -= order.Package.Price;
        order.StatusId = StatusIdConst.PAID;


        // Kafka event yuborish
        var eventData = new OrderPaidEvent
        {
            OrderId = order.Id,
            CustomerId = order.CustomerId,
            PackageId = order.PackageId,
            TrafficAmountMb = order.Package.TrafficAmountMb
        };
        string jsonValue = JsonSerializer.Serialize(eventData);

        await _producer.ProduceAsync("order-paid", new Message<Null, string>
        {
            Value = jsonValue
        });

        await _context.SaveChangesAsync();
        return order;
    }
}

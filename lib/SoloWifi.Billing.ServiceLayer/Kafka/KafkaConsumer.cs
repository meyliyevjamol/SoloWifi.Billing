using Confluent.Kafka;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SoloWifi.Billing.DataLayer;
using SoloWifi.Billing.ServiceLayer.Services.OrderSevices;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SoloWifi.Billing.ServiceLayer;

public class KafkaConsumer : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ConsumerConfig _config;

    public KafkaConsumer(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _config = new ConsumerConfig
        {
            BootstrapServers = "localhost:9092",
            GroupId = "update-service",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.Run(() =>
        {
            using var consumer = new ConsumerBuilder<Null, string>(_config).Build();
            consumer.Subscribe("order-paid");

            while (!stoppingToken.IsCancellationRequested)
            {
                var cr = consumer.Consume(stoppingToken);
                OrderPaidEvent msg = JsonSerializer.Deserialize<OrderPaidEvent>(cr.Message.Value);


                using var scope = _serviceScopeFactory.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<EfCoreContext>();

                var entity = db.Orders.Include(s => s.Customer).Include(s => s.Package).FirstOrDefault(x => x.Id == msg.OrderId);
                if (entity != null)
                {
                    entity.Customer.TotalMb += msg.TrafficAmountMb;
                    db.SaveChanges();
                }
            }
        });
    }
}

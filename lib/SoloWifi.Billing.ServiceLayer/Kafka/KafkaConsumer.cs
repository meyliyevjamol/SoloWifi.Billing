using Confluent.Kafka;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SoloWifi.Billing.DataLayer;
using System.Text.Json;

namespace SoloWifi.Billing.ServiceLayer;

public class KafkaConsumer : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ConsumerConfig _config;
    private readonly ILogger<KafkaConsumer> _logger;

    public KafkaConsumer(IServiceScopeFactory serviceScopeFactory, ILogger<KafkaConsumer> logger)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;

        _config = new ConsumerConfig
        {
            BootstrapServers = "kafka:29092",
            GroupId = "update-service",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.Run(async () =>
        {
            using var consumer = new ConsumerBuilder<Null, string>(_config).Build();
            consumer.Subscribe("order-paid");

            _logger.LogInformation("Kafka consumer started and subscribed to topic 'order-paid'.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var cr = consumer.Consume(stoppingToken);
                    var msg = JsonSerializer.Deserialize<OrderPaidEvent>(cr.Message.Value);

                    _logger.LogInformation("Received message: OrderId={OrderId}, TrafficAmountMb={TrafficAmountMb}, Partition={Partition}, Offset={Offset}",
                        msg.OrderId, msg.TrafficAmountMb, cr.Partition, cr.Offset);

                    using var scope = _serviceScopeFactory.CreateScope();
                    var db = scope.ServiceProvider.GetRequiredService<EfCoreContext>();

                    var entity = await db.Orders
                        .Include(s => s.Customer)
                        .Include(s => s.Package)
                        .Include(s => s.Status)
                        .FirstOrDefaultAsync(x => x.Id == msg.OrderId);

                    if (entity != null)
                    {
                        entity.Customer.TotalMb += msg.TrafficAmountMb;
                        await db.SaveChangesAsync();

                        _logger.LogInformation("Order {OrderId} updated successfully. Customer {CustomerId} new TotalMb: {TotalMb}",
                            entity.Id, entity.Customer.Id, entity.Customer.TotalMb);
                    }
                    else
                    {
                        _logger.LogWarning("Order {OrderId} not found in database.", msg.OrderId);
                    }
                }
                catch (OperationCanceledException)
                {
                    // stoppingToken cancel qilinsa xatolik emas
                    _logger.LogInformation("Kafka consumer stopping due to cancellation.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing Kafka message.");
                }
            }
        }, stoppingToken);
    }
}

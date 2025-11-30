using Confluent.Kafka;
using Microsoft.EntityFrameworkCore;
using SoloWifi.Billing.DataLayer;
using SoloWifi.Billing.DataLayer.Repositories;
using SoloWifi.Billing.ServiceLayer;
using SoloWifi.Billing.ServiceLayer.Kafka;
using SoloWifi.Billing.ServiceLayer.Kafka.Producer;
using SoloWifi.Billing.ServiceLayer.Services;

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Services
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // Database
    builder.Services.AddDbContext<EfCoreContext>(options =>
        options.UseNpgsql("Host=localhost;Port=5432;Username=postgres;Password=postgres;Database=solo-wifi;"));

    // Application Services
    builder.Services.AddScoped<IOrderService, OrderService>();

    //// Repositories
    builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
    builder.Services.AddScoped<IPackageRepository, PackageRepository>();
    builder.Services.AddScoped<IOrderRepository, OrderRepository>();

    //// Kafka
   
    builder.Services.AddSingleton<IProducer<Null, string>>(sp =>
    {
        var config = new ProducerConfig
        {
            BootstrapServers = "localhost:9092"
        };
        return new ProducerBuilder<Null, string>(config).Build();
    });

    builder.Services.AddHostedService<KafkaConsumer>();

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();

    app.Run();

}
catch (Exception ex)
{
    Console.WriteLine("=== XATO! ===");
    Console.WriteLine($"Xato: {ex.Message}");
    Console.WriteLine($"Stack Trace: {ex.StackTrace}");
    Console.WriteLine($"Inner Exception: {ex.InnerException?.Message}");
    Console.ReadKey();
}

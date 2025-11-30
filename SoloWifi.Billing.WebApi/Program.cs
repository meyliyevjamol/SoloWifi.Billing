using Confluent.Kafka;
using Microsoft.EntityFrameworkCore;
using SoloWifi.Billing.DataLayer;
using SoloWifi.Billing.ServiceLayer;
using SoloWifi.Billing.ServiceLayer.Services;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// Database
builder.Services.AddDbContext<EfCoreContext>(options =>
    options.UseNpgsql(connectionString));

// Application Services
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IPackageService, PackageService>();

//// Repositories
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IPackageRepository, PackageRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

//// Kafka

builder.Services.AddSingleton<IProducer<Null, string>>(sp =>
{
    var config = new ProducerConfig
    {
        BootstrapServers = "kafka:29092"
    };
    return new ProducerBuilder<Null, string>(config).Build();
});

builder.Services.AddHostedService<KafkaConsumer>();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

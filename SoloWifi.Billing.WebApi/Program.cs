using Confluent.Kafka;
using Microsoft.EntityFrameworkCore;
using SoloWifi.Billing.DataLayer;
using SoloWifi.Billing.ServiceLayer;

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

//// Repositories
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IPackageRepository, PackageRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

//// Kafka

var kafkaConfig = new Confluent.Kafka.ProducerConfig
{
    BootstrapServers = builder.Configuration["Kafka:BootstrapServers"]
};
builder.Services.AddSingleton(kafkaConfig);

builder.Services.AddHostedService<KafkaConsumer>();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

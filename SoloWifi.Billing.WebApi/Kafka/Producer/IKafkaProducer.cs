namespace SoloWifi.Billing.ServiceLayer.Kafka.Producer;

public interface IKafkaProducer
{
    Task ProduceAsync<T>(string topic, T message);
}

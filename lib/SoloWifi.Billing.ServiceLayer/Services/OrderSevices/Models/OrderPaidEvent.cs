namespace SoloWifi.Billing.ServiceLayer;

public class OrderPaidEvent
{
   public long         OrderId {  get; set; }
   public long CustomerId {  get; set; }
   public long PackageId {  get; set; }
   public decimal TrafficAmountMb {  get; set; }
}

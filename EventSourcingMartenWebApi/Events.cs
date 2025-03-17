namespace EventSourcingMartenWebApi;

public class Events
{
    public record OrderCreated
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string ProductName { get; set; }
        public string DeliveryAddress { get; set; }
    }

    public class OrderAddressUpdated
    {
        public Guid Id { get; set; }
        public string DeliveryAddress { get; set; }
    }

    public class OrderDispatched
    {
        public Guid Id { get; set; }
        public DateTime DisptchedAtUtc { get; set; }
    }

    public class OrderOutForDelivery
    {
        public Guid Id { get; set; }
        public DateTime OutForDeliveryAtUtc { get; set; }
    }

    public class OrderDelivered
    {
        public Guid Id { get; set; }
        public DateTime DeliveredAtUtc { get; set; }
    }
}

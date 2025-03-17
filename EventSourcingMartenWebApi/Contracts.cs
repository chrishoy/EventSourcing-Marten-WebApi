namespace EventSourcingMartenWebApi;

public class Contracts
{
    public class CreateOrderRequest
    {
        public string ProductName { get; set; }
        public string DeliveryAddress { get; set; }
    }

    public class DeliveryAddressUpdateRequest
    {
        public string DeliveryAddress { get; set; }
    }

    public class OrderDispatched
    {
        public DateTime DispatchedAtUtc { get; set; }
    }

    public class OrderOutForDelivery
    {
        public DateTime OutForDeliveryAtUtc { get; set; }
    }

    public class OrderDelivered
    {
        public DateTime DeliveredAtUtc { get; set; }
    }
}

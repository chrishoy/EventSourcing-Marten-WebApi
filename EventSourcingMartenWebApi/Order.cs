namespace EventSourcingMartenWebApi
{
    public sealed record Order(
        Guid Id, 
        string ProductName, 
        string DeliveryAddress,
        DateTime? DispatchedAtUtc,
        DateTime? OutForDeliveryAtUtc,
        DateTime? DeliveredAtUtc,
        DateTime? Delivered)
    {
        public static Order Create(Events.OrderCreated created) =>
            new(created.Id, created.ProductName, created.DeliveryAddress, null, null, null, null);

        public static Order Apply(Events.OrderAddressUpdated addressUpdated, Order order) =>
            order with
            {
                DeliveryAddress = addressUpdated.DeliveryAddress
            };

        public static Order Apply(Events.OrderDispatched dispatched, Order order) =>
            order with
            {
                DispatchedAtUtc = dispatched.DisptchedAtUtc
            };

        public static Order Apply(Events.OrderOutForDelivery outForDelivery, Order order) =>
            order with
            {
                OutForDeliveryAtUtc = outForDelivery.OutForDeliveryAtUtc
            };

        public static Order Apply(Events.OrderDelivered delivered, Order order) =>
            order with
            {
                DeliveredAtUtc = delivered.DeliveredAtUtc
            };
    }
}

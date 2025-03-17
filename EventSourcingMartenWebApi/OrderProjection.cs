using Marten.Events.Aggregation;

namespace EventSourcingMartenWebApi;

public class OrderProjection: SingleStreamProjection<Order>
{
    public Order Create(Events.OrderCreated created) =>
        new Order(created.Id, created.ProductName, created.DeliveryAddress, null, null, null, null);

    public void Apply(Events.OrderAddressUpdated addressUpdated, Order order) =>
        order = order with
        {
            DeliveryAddress = addressUpdated.DeliveryAddress
        };

    public void Apply(Events.OrderDispatched dispatched, Order order) =>
        order = order with
        {
            DispatchedAtUtc = dispatched.DisptchedAtUtc
        };

    public void Apply(Events.OrderOutForDelivery outForDelivery, Order order) =>
        order = order with
        {
            OutForDeliveryAtUtc = outForDelivery.OutForDeliveryAtUtc
        };

    public void Apply(Events.OrderDelivered delivered, Order order) =>
        order = order with
        {
            DeliveredAtUtc = delivered.DeliveredAtUtc
        };
}

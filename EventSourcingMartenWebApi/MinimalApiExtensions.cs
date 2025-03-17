using Marten;
using static EventSourcingMartenWebApi.Contracts;

namespace EventSourcingMartenWebApi;

internal static class MinimalApiExtensions
{
    public static WebApplication MapMinimalApis(this WebApplication app)
    {
        app.MapGet("orders", async (IQuerySession session) =>
        {
            var orders = await session.Query<Order>().ToListAsync();
            return Results.Ok(orders);
        });

        app.MapGet("orders/{orderId:guid}", async (IQuerySession session, Guid orderId) =>
        {
            // Builds the order from the AggregateStream
            //var order = await session.Events.AggregateStreamAsync<Order>(orderId);

            // Loads the latest projection of the order
            var order = await session.LoadAsync<Order>(orderId);

            return order is not null ? Results.Ok(order) : Results.NotFound();
        });

        app.MapPost("orders", async (IDocumentStore store, CreateOrderRequest request) =>
        {
            var @event = new Events.OrderCreated
            {
                ProductName = request.ProductName,
                DeliveryAddress = request.DeliveryAddress
            };

            await using var session = store.LightweightSession();
            session.Events.StartStream<Order>(@event.Id, @event);
            await session.SaveChangesAsync();
            return Results.Ok(@event);
        });

        app.MapPost("orders/{orderId:guid}/address", async (IDocumentStore store, Guid orderId, DeliveryAddressUpdateRequest request) =>
        {
            var @event = new Events.OrderAddressUpdated
            {
                Id = orderId,
                DeliveryAddress = request.DeliveryAddress
            };

            await using var session = store.LightweightSession();
            session.Events.Append(orderId, @event);
            await session.SaveChangesAsync();
            return Results.Ok(@event);
        });


        app.MapPost("orders/{orderId:guid}/dispatch", async (IDocumentStore store, Guid orderId, OrderDispatched request) =>
        {
            var @event = new Events.OrderDispatched
            {
                Id = orderId,
                DisptchedAtUtc = request.DispatchedAtUtc
            };

            await using var session = store.LightweightSession();
            session.Events.Append(orderId, @event);
            await session.SaveChangesAsync();
            return Results.Ok(@event);
        });

        app.MapPost("orders/{orderId:guid}/out-for-delivery", async (IDocumentStore store, Guid orderId, OrderOutForDelivery request) =>
        {
            var @event = new Events.OrderOutForDelivery
            {
                Id = orderId,
                OutForDeliveryAtUtc = request.OutForDeliveryAtUtc
            };

            await using var session = store.LightweightSession();
            session.Events.Append(orderId, @event);
            await session.SaveChangesAsync();
            return Results.Ok(@event);
        });

        app.MapPost("orders/{orderId:guid}/delivered", async (IDocumentStore store, Guid orderId, OrderDelivered request) =>
        {
            var @event = new Events.OrderDelivered
            {
                Id = orderId,
                DeliveredAtUtc = request.DeliveredAtUtc
            };

            await using var session = store.LightweightSession();
            session.Events.Append(orderId, @event);
            await session.SaveChangesAsync();
            return Results.Ok(@event);
        });

        return app;
    }
}

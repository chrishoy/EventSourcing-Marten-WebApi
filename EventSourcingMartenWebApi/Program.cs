using EventSourcingMartenWebApi;
using Marten;
using Marten.Events.Projections;
using Marten.Linq.Parsing.Operators;
using Weasel.Core;
using static EventSourcingMartenWebApi.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add Marten to the services collection
builder.Services.AddMarten(options => 
{
    string cs = builder.Configuration.GetConnectionString("DefaultConnection")!;
    options.Connection(cs);
    options.UseSystemTextJsonForSerialization();

    // Build the OrderProjection as the events are applied
    options.Projections.Add<OrderProjection>(ProjectionLifecycle.Inline);

    // Creates the schema objects if they do not exist
    if (builder.Environment.IsDevelopment())
    {
        options.AutoCreateSchemaObjects = AutoCreate.All;
    }
});

// Add controllers to the container.
builder.Services.AddControllers();

// Add Swagger to the container. Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Minimal API's for the Order API
app.MapMinimalApis();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

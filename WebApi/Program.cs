

using Application.Consumers;
using Application.Extensions;
using Infrastructure;
using Infrastructure.Database.MongoDb;
using market_price_ingestor_service;
using MassTransit;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);

// Add Masstransit
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<TickerCollectedConsumer>();
    x.UsingRabbitMq( (context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ReceiveEndpoint("Events:ITickerCollected", e =>
        {
            e.Consumer<TickerCollectedConsumer>(context);
        });
    });
});
builder.Services.AddMassTransitHostedService();

// MongoDb
builder.Services.Configure<MarketPriceLakeDatabaseConfiguration>(
builder.Configuration.GetSection("MarketPriceLakeDatabase"));

builder.Services.AddHostedService<StartupConfiguration>();

// Serilog
builder.Host.UseSerilog((host, log) =>
{
    if (host.HostingEnvironment.IsProduction())
        log.MinimumLevel.Information();
    else
        log.MinimumLevel.Debug();

    log.MinimumLevel.Override("Microsoft", LogEventLevel.Warning);
    log.MinimumLevel.Override("Quartz", LogEventLevel.Information);
    log.WriteTo.Console();
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();
builder.Services.Configure<HealthCheckPublisherOptions>(options =>
{
    options.Delay = TimeSpan.FromSeconds(2);
    options.Predicate = (check) => check.Tags.Contains("ready");
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapControllers();

app.UseRouting();

app.UseAuthorization();
//app.MapHealthChecks("/health");
app.UseEndpoints(endpoints =>
{
    endpoints.MapHealthChecks("/health/ready", new HealthCheckOptions()
    {
        Predicate = (check) => check.Tags.Contains("ready"),
    });

    endpoints.MapHealthChecks("/health/live", new HealthCheckOptions());
});


app.Run();
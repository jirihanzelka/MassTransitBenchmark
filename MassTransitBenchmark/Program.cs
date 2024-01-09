using MassTransit;
using MassTransitBenchmark;
using MassTransitBenchmark.Helpers;
using MassTransitBenchmark.RabbitMq;

var builder = WebApplication.CreateBuilder();

// mass transit example
// builder.Services.AddMassTransit(x =>
// {
//     x.AddConsumers(typeof(Program).Assembly);
//     
//     // in memory for DEV
//     // x.UsingInMemory((context, cfg) =>
//     // {
//     //     cfg.ConfigureEndpoints(context);
//     // });
//     
//     // rabbitmq for PROD
//     x.UsingRabbitMq((context, configurator) =>
//     {
//         configurator.Host("localhost", "/", h =>
//         {
//             h.Username("guest");
//             h.Password("guest");
//         });
//         configurator.ConfigureEndpoints(context);
//     });
//     
//     // x.UsingGrpc((context, cfg) =>
//     // {
//     //     cfg.Host(h =>
//     //     {
//     //         h.Host = "127.0.0.1";
//     //         h.Port = 19796;
//     //     });
//     //
//     //     cfg.ConfigureEndpoints(context);
//     // });
// });
//
// builder.Services.AddHostedService<BusPublisher>();

// just rabbitmq example
builder.Services.AddHostedService<RabbitPublisher>();
builder.Services.AddHostedService<RabbitConsumer>();
builder.Services.AddSingleton<Statistics>();

var app = builder.Build();
app.Run("http://localhost:6054");
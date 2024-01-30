using Discount.Grpc.Repositories.Interfaces;
using Discount.Grpc.Repositories;
using Discount.Grpc.Services;
using Discount.API.Seeding;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();
builder.Services.AddGrpc();

var logger = builder.Services.BuildServiceProvider().GetService<ILogger<Program>>();
DatabaseSeeder.SeedDatabase(builder.Configuration, logger);


var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
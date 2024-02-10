using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Cache.CacheManager;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddOcelot().AddCacheManager(settings => settings.WithDictionaryHandle());

builder.Host.ConfigureAppConfiguration((context, config) =>
{
    config.AddJsonFile($"ocelot.{context.HostingEnvironment.EnvironmentName}.json", true, true);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
/*if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}*/

await app.UseOcelot();

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

app.Run();

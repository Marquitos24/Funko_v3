using System.Text.Json.Serialization;
using Funkos.Routes;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

var app = builder.Build();

app.MapFunkosRoutes();   // Todas las rutas de productos

app.Run();


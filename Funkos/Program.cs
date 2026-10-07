using System.Text.Json.Serialization;
using Funkos.Repositories;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers()
    .AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.AddSingleton<IFunkoRepository, FunkoRepository>();
var app = builder.Build();

app.MapControllers();
app.Run();


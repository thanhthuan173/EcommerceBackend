using BeverageBackend.API.Extensions;
using BeverageBackend.Application;
using BeverageBackend.Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.AddSerilogLogging();

builder.Services.AddApplicationLayer();
builder.Services.AddInfrastructureLayer(builder.Configuration);
builder.Services.AddApiLayer(builder.Configuration);

var app = builder.Build();

app.UseSerilogRequestLogging();
app.SeedDataIfRequested(args);
app.UseApiMiddlewares();
app.MapControllers();

app.Run();

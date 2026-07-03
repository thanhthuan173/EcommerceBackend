using EcommerceBackend.API.Extensions;
using EcommerceBackend.Application;
using EcommerceBackend.Infrastructure;
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

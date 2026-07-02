using BeverageBackend.API.Configurations;
using BeverageBackend.API.Extensions;
using BeverageBackend.API.Middlewares;
using BeverageBackend.API.Services;
using BeverageBackend.Application;
using BeverageBackend.Application.Interfaces;
using BeverageBackend.Application.Interfaces.Services;
using BeverageBackend.Application.Services;
using BeverageBackend.Application.Services.Auth;
using BeverageBackend.Application.Validators.Auth;
using BeverageBackend.Infrastructure;
using BeverageBackend.Infrastructure.Persistence;
using BeverageBackend.Infrastructure.Repository;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;

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

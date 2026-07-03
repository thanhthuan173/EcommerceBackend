using EcommerceBackend.API.Middlewares;
using EcommerceBackend.Infrastructure.Persistence;

namespace EcommerceBackend.API.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication UseApiMiddlewares(this WebApplication app)
    {
        // Enable Swagger only in Development.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // Global exception handling
        app.UseMiddleware<ExceptionMiddleware>();

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();

        return app;
    }

    public static WebApplication SeedDataIfRequested(this WebApplication app, string[] args)
    {
        if (args.Length == 1 && args[0].Equals("seeddata", StringComparison.OrdinalIgnoreCase))
        {
            using var scope = app.Services.CreateScope();
            var seed = scope.ServiceProvider.GetRequiredService<Seed>();
            seed.SeedData();
        }

        return app;
    }
}
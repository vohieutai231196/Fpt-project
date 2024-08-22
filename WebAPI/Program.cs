using Infrastructure;
using Application;
using Infrastructure.Context;
using Domain;
using Restaurants.API.Extensions;
using Microsoft.AspNetCore.Identity;
using WebApi.Middlewares;
using WebApi;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using OpenTelemetry.Logs;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.AddPresentation();

builder.Services.AddIdentityApiEndpoints<Users>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDBContext>();

builder.Services.AddSingleton(TimeProvider.System);
//Configure Redis
var cacheSettings = builder.Services.GetCacheSettings(builder.Configuration);
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = cacheSettings.DistinationUrl;
});
builder.Services.AddSingleton<Instrumentation>();
//Open telementry;
builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource => resource.AddService(
        serviceName: "property-server",
        serviceVersion: "1.0.0"))
    .WithTracing(tracing => tracing
        .AddSource("property-server")
        .AddAspNetCoreInstrumentation()
        .AddConsoleExporter());

builder.Logging.AddOpenTelemetry(options => options
    .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(
        serviceName: "property-server",
        serviceVersion: "1.0.0"))
    .AddConsoleExporter());

builder.Services.AddControllers();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(opt => opt.DisplayRequestDuration());
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseMiddleware<ErrorHandlingMiddleware>();
app.MapGroup("api/identity").MapIdentityApi<Users>();
app.Run();

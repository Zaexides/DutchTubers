using DutchTubers.Server.Services;
using Microsoft.Extensions.FileProviders;
using TwitchLib.Api;
using TwitchLib.Api.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<ICacheProvider, CacheProvider>();
builder.Services.AddScoped<ISecretProvider, SecretProvider>();
builder.Services.AddScoped<IVTuberListProvider, VTuberListProvider>();
builder.Services.AddScoped<ITwitchAPI, TwitchAPI>();
builder.Services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
builder.Services.AddScoped<ITwitchService, TwitchService>();
builder.Services.AddScoped<IRandomProvider, RandomProvider>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

var fileProvider = new PhysicalFileProvider($"{builder.Environment.ContentRootPath}/ClientApp/dist");

app.UseFileServer(new FileServerOptions()
{
    FileProvider = fileProvider
});

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapFallbackToFile("index.html", new StaticFileOptions()
    {
        FileProvider = fileProvider
    });
});

app.Run();

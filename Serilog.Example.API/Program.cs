using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Context;
using Serilog.Exceptions;
using Serilog.Formatting.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

builder.Logging.ClearProviders();

Log.Logger = new LoggerConfiguration()
    .Enrich.WithProperty("Environment", "Development")
    .Enrich.FromLogContext()
    .Enrich.WithExceptionDetails()
    .Enrich.WithMachineName()
    .WriteTo.Console(formatter: new JsonFormatter())
    .CreateLogger();

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSerilogRequestLogging();
app.Use(async (context, next) =>
{
    using (LogContext.PushProperty("OrderNumber", "123"))
        await next.Invoke();
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

app.Run();
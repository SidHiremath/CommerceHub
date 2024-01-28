using Data;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Sid.CommerceHub.Business;
using Sid.CommerceHub.OrderService.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();


/*
builder.Services.AddHttpClient<IProductService, Sid.CommerceHub.OrderService.ProductService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ProductServiceUrl"] ??
                                 throw new Exception("ProductServiceUrl is need to be set"));
}); */

var loggerConfiguration = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(loggerConfiguration);

builder.Services.AddDbContext<DatabaseContext>(options =>
    options
        .UseNpgsql(
            "Host=localhost;Port=5432;Database=postgres;Username=sileon;Password=Pass@word")
        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

builder.Services.AddValidatorsFromAssemblyContaining<OrderCreateValidator>(ServiceLifetime.Transient);
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddTransient<IDatabaseService, DatabaseService>();


// Add Controllers
// Dependency Injection - services, HttpClient, DbContext
// Swagger


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options =>
        options.RouteTemplate = "/docs/{documentName}/swagger.json");
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/docs/v1/swagger.json", "v1");
        options.RoutePrefix = "docs";
    });
}

app.MapControllers();


app.UseHttpsRedirection();

app.Run();
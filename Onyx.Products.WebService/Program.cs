using Onyx.Products.WebService.Core;
using Onyx.Products.WebService.Core.Database;
using Onyx.Products.WebService.Core.Events;
using Onyx.Products.WebService.Database;
using Onyx.Products.WebService.Events;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

//Core
builder.Services.AddTransient<IColourStore, ColourStore>();
builder.Services.AddTransient<IProductStore, ProductStore>();
builder.Services.AddTransient<IProductValidator, ProductValidator>();

//Database
builder.Services.AddTransient<IColourData, ColourData>();
builder.Services.AddTransient<IProductData, ProductData>();

//Events
builder.Services.AddTransient<IProductEvents, ProductEvents>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

using BangazonBE.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;

var builder = WebApplication.CreateBuilder(args);

// allows passing datetimes without time zone data 
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// allows our api endpoints to access the database through Entity Framework Core
builder.Services.AddNpgsql<BangazonBEDbContext>(builder.Configuration["BangazonBEDbConnectionString"]);

// Set the JSON serializer options
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// GET Products
app.MapGet("/api/products", (BangazonBEDbContext db) =>
{
    return db.Products.ToList();
});

// GET Types
app.MapGet("/api/producttypes", (BangazonBEDbContext db) =>
{
    return db.ProductTypes.ToList();
});

// GET ProductOrders
app.MapGet("/api/productorders", (BangazonBEDbContext db) =>
{
    return db.ProductOrders.ToList();
});

// GET Orders
app.MapGet("/api/orders", (BangazonBEDbContext db) =>
{
    return db.Orders.ToList();
});

// GET Types
app.MapGet("/api/customers", (BangazonBEDbContext db) =>
{
    return db.Customers.ToList();
});

// GET Users
app.MapGet("/api/users", (BangazonBEDbContext db) =>
{
    return db.Users.ToList();
});

// GET Sellers
app.MapGet("/api/sellers", (BangazonBEDbContext db) =>
{
    return db.Sellers.ToList();
});

app.Run();

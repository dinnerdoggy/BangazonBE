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

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy.WithOrigins("http://localhost:3000") // Allow requests from frontend
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

var app = builder.Build();

app.UseCors("AllowFrontend"); // Apply CORS policy

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ******* ENDPOINTS ********

// Check user
app.MapPost("/api/checkuser", async (BangazonBEDbContext db, CheckUserDto data) =>
{
var user = await db.Users.FirstOrDefaultAsync(u => u.Uid == data.Uid);

if (user == null)
{
return Results.NotFound(new { message = "User not found" });
}

return Results.Ok(user);
});

// Register user
app.MapPost("/api/register", async (BangazonBEDbContext db, RegisterUserDto data) =>
{
    // Ensure required fields are present
    if (string.IsNullOrWhiteSpace(data.Uid) || string.IsNullOrWhiteSpace(data.UserName))
    {
        return Results.BadRequest(new { message = "Uid and UserName are required." });
    }

    // Check if user already exists
    var existingUser = await db.Users.FirstOrDefaultAsync(u => u.Uid == data.Uid);
    if (existingUser != null)
    {
        return Results.Conflict(new { message = "User already exists" });
    }

    // Create new user
    var newUser = new User
    {
        Uid = data.Uid,
        UserName = data.UserName  // Ensure this is set!
    };

    db.Users.Add(newUser);
    await db.SaveChangesAsync();

    return Results.Ok(newUser);
});



// GET Products
app.MapGet("/api/products", (BangazonBEDbContext db) =>
{
    return db.Products.ToList();
});

// GET Product by ID
app.MapGet("/api/products/{id}", (BangazonBEDbContext db, int id) =>
{
    try
    {
        return Results.Ok(db.Products.Include(c => c.ProductType).Single(c => c.Id == id));
    }
    catch
    {
        return Results.NotFound();
    }
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

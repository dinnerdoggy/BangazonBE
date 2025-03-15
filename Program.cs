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

app.MapPost("/api/orders/add", async (BangazonBEDbContext db, ProductOrderDto data) =>
{
    // Get or create an active order
    var order = await db.Orders
        .Include(o => o.ProductOrders)
        .FirstOrDefaultAsync(o => o.CustomerId == data.CustomerId && o.Pending);

    if (order == null)
    {
        order = new Order { CustomerId = data.CustomerId, Pending = true, Total = 0 };
        db.Orders.Add(order);
        await db.SaveChangesAsync();
    }

    // Check if product is already in order
    var ProductOrders = order.ProductOrders.FirstOrDefault(op => op.ProductId == data.ProductId);
    if (ProductOrders != null)
    {
        ProductOrders.Quantity += 1;  // Increase quantity if already in cart
    }
    else
    {
        db.ProductOrders.Add(new ProductOrders { OrderId = order.Id, ProductId = data.ProductId, Quantity = 1 });
    }

    // Update total
    var product = await db.Products.FindAsync(data.ProductId);
    order.Total += product.Price;

    await db.SaveChangesAsync();
    return Results.Ok(order);
});

app.MapGet("/api/orders/{customerId}", async (BangazonBEDbContext db, int customerId) =>
{
    // Find the active (pending) order for the customer
    var order = await db.Orders
        .Include(o => o.ProductOrders) // Ensure product orders are included
        .ThenInclude(po => po.Product) // Include product details
        .FirstOrDefaultAsync(o => o.CustomerId == customerId && o.Pending);

    if (order == null)
    {
        return Results.NotFound(new { message = "No active cart found for this customer." });
    }

    // Transform the data to return only relevant details
    var cartItems = order.ProductOrders.Select(po => new
    {
        po.Product.Id,
        po.Product.ProductName,
        po.Quantity,
        po.Product.Price
    }).ToList();

    return Results.Ok(cartItems);
});



app.Run();

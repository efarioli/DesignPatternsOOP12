using DesignPatterns12.Application.Interfaces;
using DesignPatterns12.Application.Services;
using DesignPatterns12.Domain.Entities;
using DesignPatterns12.Infrastructure.Data;
using DesignPatterns12.Infrastructure.Repositories;
using DesignPatterns12.WebApi.Dtos;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// --------------------
// Database (SQLite)
// --------------------
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// --------------------
// Dependency Injection
// --------------------
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUserService, UserService>();


// --------------------
// Swagger / OpenAPI
// --------------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// --------------------
// API Endpoints
// --------------------

// Health check
app.MapGet("/health", () => Results.Ok("API is running ??"));

// --------------------
// GET: All products
// --------------------
app.MapGet("/api/products", async (IProductService service) =>
{
    var products = await service.GetAllAsync();
    return Results.Ok(products);
});

// --------------------
// GET: One product by Id
// --------------------
app.MapGet("/api/products/{id:guid}", async (IProductService service, Guid id) =>
{
    var product = await service.GetByIdAsync(id);
    return product is not null ? Results.Ok(product) : Results.NotFound();
});

// --------------------
// POST: Create product
// --------------------
app.MapPost("/api/products", async (ProductCreateDto dto, IProductService service) =>
{
    if (string.IsNullOrWhiteSpace(dto.Name))
        return Results.BadRequest("Product name is required.");

    if (dto.Price is null || dto.Price <= 0)
        return Results.BadRequest("Product price must be greater than 0.");

    var product = new Product(dto.Name, dto.Price.Value);

    await service.CreateAsync(product);

    return Results.Created($"/api/products/{product.Id}", product);
});

// --------------------
// PUT: Update product
// --------------------
app.MapPut("/api/products/{id:guid}", async (Guid id, ProductCreateDto dto, IProductService service) =>
{
    var product = await service.GetByIdAsync(id);
    if (product is null)
        return Results.NotFound();

    if (!string.IsNullOrWhiteSpace(dto.Name))
        product.UpdateName(dto.Name);

    if (dto.Price is not null && dto.Price > 0)
        product.UpdatePrice(dto.Price.Value);

    var updated = await service.UpdateAsync(id, product);
    return updated ? Results.NoContent() : Results.Problem("Could not update product.");
});

// --------------------
// DELETE: Remove product
// --------------------
app.MapDelete("/api/products/{id:guid}", async (Guid id, IProductService service) =>
{
    var deleted = await service.DeleteAsync(id);
    return deleted ? Results.NoContent() : Results.NotFound();
});


// =======================
// USERS ENDPOINTS
// =======================

app.MapGet("/api/users", async (IUserService service) =>
{
    var users = await service.GetAllAsync();
    return Results.Ok(users);
});

app.MapGet("/api/users/{id:guid}", async (IUserService service, Guid id) =>
{
    var user = await service.GetByIdAsync(id);
    return user is not null ? Results.Ok(user) : Results.NotFound();
});

app.MapPost("/api/users", async (UserCreateDto dto, IUserService service) =>
{
    if (string.IsNullOrWhiteSpace(dto.Username) || string.IsNullOrWhiteSpace(dto.Email))
        return Results.BadRequest("Name and Email are required.");

    var user = new User(dto.Username, dto.Email);
    await service.CreateAsync(user);

    return Results.Created($"/api/users/{user.Id}", user);
});

app.MapPut("/api/users/{id:guid}", async (Guid id, UserCreateDto dto, IUserService service) =>
{
    var updated = new User(dto.Username, dto.Email);
    var success = await service.UpdateAsync(id, updated);
    return success ? Results.Ok() : Results.NotFound();
});

app.MapDelete("/api/users/{id:guid}", async (Guid id, IUserService service) =>
{
    var success = await service.DeleteAsync(id);
    return success ? Results.Ok() : Results.NotFound();
});

// --------------------
// Run the app
// --------------------
app.Run();

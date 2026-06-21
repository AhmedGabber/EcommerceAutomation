using EcommerceAi.Application.AI;
using EcommerceAi.Application.IServices;
using EcommerceAi.Application.Services;
using EcommerceAi.Application.Tools;
using EcommerceAi.Infrastructure.DBContext;
using EcommerceAi.Infrastructure.Repositories.Implimentation;
using EcommerceAi.Infrastructure.Repositories.Interfaces;
using EcommerceAi.Infrastructure.Seed;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();

builder.Services.AddScoped<IInventoryService, InventoryService>();

builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

builder.Services.AddScoped<ICustomerService, CustomerService>();

builder.Services.AddScoped<IShipmentRepository,ShipmentRepository>();

builder.Services.AddScoped<IShipmentService,ShipmentService>();

builder.Services.AddScoped<IAIService,AIService>();

builder.Services.AddHttpClient<IOllamaService,
    OllamaService>(client =>
    {
        client.BaseAddress =
            new Uri("http://localhost:11434");
    });

builder.Services.AddScoped<IAIServiceTool, GetProductsTool>();

builder.Services.AddScoped<IAIServiceTool, TrackShipmentTool>();

builder.Services.AddScoped<IToolRegistry, ToolRegistry>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<AppDbContext>();

    await DbInitializer.SeedAsync(context);
}

app.Run();
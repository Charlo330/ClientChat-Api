using chatApi;
using chatApi.Managers;
using chatApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(); // Add controller services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<CommandHandler>();
builder.Services.AddScoped<WebsocketHandler>();
builder.Services.AddSingleton<WebsocketManager>();
builder.Services.AddSingleton<RoomService>();
builder.Services.AddTransient<RoomManager>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization(); // Add middleware for authorization if needed

app.MapControllers(); // Map attribute-defined routes in controllers

app.UseWebSockets();

app.Map("/ws", async context =>
    {
        using var scope = context.RequestServices.CreateScope();
        var websocketHandler = scope.ServiceProvider.GetRequiredService<WebsocketHandler>();
        await websocketHandler.HandleWebSocketAsync(context);
    });

app.Run();
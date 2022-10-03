using GameServer.Services.SocketServer;
using System.Diagnostics;
Console.WriteLine("Application is starting");
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var socketService = new SocketService();
socketService.InitSockets(5);

Console.WriteLine("Application started");

builder.Services.AddControllers();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
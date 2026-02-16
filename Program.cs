using Microsoft.EntityFrameworkCore;
using PinjamRuanganAPI.Data;
using DotNetEnv;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var server = Environment.GetEnvironmentVariable("DB_SERVER")
    ?? throw new Exception("DB_SERVER tidak ditemukan di .env");

var db = Environment.GetEnvironmentVariable("DB_NAME")
    ?? throw new Exception("DB_NAME tidak ditemukan di .env");

var user = Environment.GetEnvironmentVariable("DB_USER")
    ?? throw new Exception("DB_USER tidak ditemukan di .env");

var password = Environment.GetEnvironmentVariable("DB_PASSWORD")
    ?? throw new Exception("DB_PASSWORD tidak ditemukan di .env");

var connectionString = $"server={server};database={db};user={user};password={password}";

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowFrontend");

app.MapControllers();

app.Run();

// Namespaces 
using Microsoft.EntityFrameworkCore;
using SimpleDotNETAPI.Middlewares;
using SimpleDotNETAPI.Interfaces;
using SimpleDotNETAPI.Repository;
using SimpleDotNETAPI.Services;
using SimpleDotNETAPI.Data;
using SimpleDotNETAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddMvc();

// Handle Dependency Injections
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IApplicationRepository, ApplicationRepository>();
builder.Services.AddScoped<ITokenRepository, TokenRepository>();

builder.Services.AddScoped<IPasswordService, PasswordService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add SQL Connection Middleware to the Application
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseMySQL(
        builder.Configuration.GetConnectionString("DefaultConnection")
    );
    options.EnableSensitiveDataLogging();
});

var app = builder.Build();

app.UseMiddleware<AuthenticationHandler>("SimpleDOTNetAPI");

app.MapControllers();

app.Run();
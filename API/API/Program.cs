using Auth.Application.Interfaces;
using Auth.Application.Services;
using Auth.DependencyInjection;
using Authorization.Application.Interfaces;
using Authorization.DependencyInjection;
using Infrastructure;
using Infrastructure.Application.Interfaces;
using Infrastructure.Application.Services;
using Infrastructure.DependencyInjection;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Seeders;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Settings.Application.Interfaces;
using System;
using System.Text;
using Users.DependencyInjection;
using Users.Domain.Entities;
using API.Application.Repository.IRepository;
using API.Application.Repository;
using API.Application.Services.IServices;
using API.Application.Services;
using API.Application.Service.ProfilService;

var builder = WebApplication.CreateBuilder(args);

 

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddOpenApi();
 
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IPermissionService, PermissionService>();
builder.Services.AddScoped<IPermissionGenerator, PermissionGenerator>();
builder.Services.AddScoped<IdentitySeeder, IdentitySeeder>();
builder.Services.AddScoped<SettingSeeder, SettingSeeder>();
builder.Services.AddScoped<ILoginTrackingService, LoginTrackingService>();
builder.Services.AddScoped<ILdapService, LdapService>();
builder.Services.AddScoped<ISettingService, SettingService>();
builder.Services.AddScoped<IProfilService, ProfilService>();
builder.Services.AddScoped<IProfilRepository, ProfilRepository>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowUse_Case_Carte", policy =>
    {
        policy
            .WithOrigins(
                "http://localhost:5239",
                "https://localhost:5229",
                "http://localhost:5074",
                "https://localhost:7014")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

builder.Services
    .AddIdentity<ApplicationUser, IdentityRole<Guid>>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();


var connectionStringSql = builder.Configuration.GetConnectionString("SqlServerConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionStringSql));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = ctx =>
        {
            Console.WriteLine($"Jwt OnAuthenticationFailed: {ctx.Exception?.Message}");
            return Task.CompletedTask;
        },
        OnTokenValidated = ctx =>
        {
            Console.WriteLine("Jwt OnTokenValidated: token valid");
            return Task.CompletedTask;
        },
        OnChallenge = ctx =>
        {
            Console.WriteLine($"Jwt OnChallenge: Error={ctx.Error} Description={ctx.ErrorDescription}");
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddAuthorization();
builder.Services.AddHttpContextAccessor(); 
 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<IdentitySeeder>();
    await seeder.SeedAsync();
}

//}
app.Use(async (context, next) =>
{
    Console.WriteLine($"{context.Request.Method} {context.Request.Path}");
    await next();
});


using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<IdentitySeeder>();
    await seeder.SeedAsync();
}

app.UseCors("AllowUse_Case_Carte");

app.UseHttpsRedirection();
        
app.UseAuthentication(); 

// Debugging: log raw Authorization header and authentication result
app.Use(async (context, next) =>
{
    var authHeader = context.Request.Headers["Authorization"].ToString();
    var isAuth = context.User?.Identity?.IsAuthenticated ?? false;
    Console.WriteLine($"Incoming Authorization: {authHeader}");
    Console.WriteLine($"HttpContext.User.Identity.IsAuthenticated: {isAuth}");
    await next();
});

app.UseAuthorization();

app.MapControllers();

app.Run();

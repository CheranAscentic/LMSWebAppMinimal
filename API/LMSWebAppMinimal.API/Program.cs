using LMSWebAppMinimal.API.Extension;
using LMSWebAppMinimal.Application.Interface;
using LMSWebAppMinimal.Application.Service;
using LMSWebAppMinimal.Data.Repository;
using LMSWebAppMinimal.Domain.Base;
using LMSWebAppMinimal.Domain.Model;
using LMSWebAppMinimal.Data.Context;
using LMSWebAppMinimal.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using LMSWebAppMinimal.Application.PermissionChecker;

var builder = WebApplication.CreateBuilder(args);

// Add OpenAPI and Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Library Management System API",
        Version = "v1",
        Description = "A .NET 9 Minimal API for Library Management System"
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddOpenApi();

// Register the DbContext
builder.Services.AddDbContext<DataDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Register UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Register repositories required by services
builder.Services.AddScoped<IRepository<BaseUser>, DatabaseRepository<BaseUser>>();
builder.Services.AddScoped<IRepository<Book>, DatabaseRepository<Book>>();

// Register application services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IBorrowingService, BorrowingService>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IPermissionChecker, PermissionChecker>();

// Register repository services
builder.Services.AddScoped<IRepository<BaseUser>, DatabaseRepository<BaseUser>>();
builder.Services.AddScoped<IRepository<Book>, DatabaseRepository<Book>>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "LMS API v1");
        options.RoutePrefix = string.Empty; // Serves Swagger UI at the app's root
    });
    app.MapOpenApi();
}

// Map all endpoints
app.RegisterAllEndpointGroups();

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.Run();


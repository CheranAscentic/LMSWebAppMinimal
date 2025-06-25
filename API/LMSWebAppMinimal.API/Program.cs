using LMSWebAppMinimal.API.Endpoint;
using LMSWebAppMinimal.Application.Interface;
using LMSWebAppMinimal.Application.Service;
using LMSWebAppMinimal.Data.Repository;
using LMSWebAppMinimal.Domain.Base;
using LMSWebAppMinimal.Domain.Model;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

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

builder.Services.AddOpenApi();

// Register repositories and services
builder.Services.AddSingleton<IRepository<Book>, InMemoryRepository<Book>>();
builder.Services.AddSingleton<IRepository<BaseUser>, InMemoryRepository<BaseUser>>();

builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IBookService, BookService>();
builder.Services.AddSingleton<IBorrowingService, BorrowingService>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "LMS API v1");
        options.RoutePrefix = string.Empty; // Serves the Swagger UI at the app's root
    });
    app.MapOpenApi();
}

// Map all endpoints
app.MapUserEndpoints();
app.MapBookEndpoints();
app.MapBorrowingEndpoints();

app.UseHttpsRedirection();

app.Run();


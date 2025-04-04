﻿using OfficePerformanceReview.API.Middleware;
using OfficePerformanceReview.Application.DependencyExtensions;
using OfficePerformanceReview.Infrastructure.DependencyExtensions;
using OfficePerformanceReview.WebAPI.DependencyExtensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenWithAuth(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

// CORS Policy Configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",
        builder => builder
            .WithOrigins("http://localhost:4200") // Ensure no wildcard (*) with AllowCredentials
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

builder.Services.AddAuthorization();
builder.Services.AddAuthenticationWithBearer(builder.Configuration);

var app = builder.Build();

app.UseCors("AllowLocalhost");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.OAuthClientId(builder.Configuration["JWT:ClientUrl"]);
        c.OAuthAppName("Swagger UI");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlerMiddleware>();

// 🔹 Correct order: Authentication first, then Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();

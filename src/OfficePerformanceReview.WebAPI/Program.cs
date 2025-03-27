using Microsoft.AspNetCore.Builder;
using OfficePerformanceReview.API.Middleware;
using OfficePerformanceReview.Application.DependencyExtensions;
using OfficePerformanceReview.Infrastructure.DependencyExtensions;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
// CORS Policy Configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",
        builder => builder
            .WithOrigins("http://localhost:4200") // Ensure no trailing slash
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()); // If credentials (cookies, etc.) are involved
});

var app = builder.Build();


app.UseCors("AllowLocalhost");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseMiddleware<ExceptionHandlerMiddleware>();

app.MapControllers();

app.Run();

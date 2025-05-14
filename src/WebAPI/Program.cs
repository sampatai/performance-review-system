using Asp.Versioning;
using Asp.Versioning.Builder;
using OfficePerformanceReview.API.Middleware;
using OfficePerformanceReview.Application.DependencyExtensions;
using OfficePerformanceReview.Infrastructure.DependencyExtensions;
using OfficePerformanceReview.WebAPI.DependencyExtensions;
using OfficePerformanceReview.WebAPI.MinimalApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer(); // For Swagger + Minimal APIs

// Add API Versioning for Minimal APIs
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true; // ✅ Needed for .ReportApiVersions() to work
    options.ApiVersionReader = new UrlSegmentApiVersionReader(); // Use URL segment versioning
})
.AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV"; // e.g. v1
    options.SubstituteApiVersionInUrl = true;
});

// Swagger with versioned support
builder.Services.AddSwaggerGenWithAuth(builder.Configuration);

// App-specific layers
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",
        policy => policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

// Auth
builder.Services.AddAuthorization();
builder.Services.AddAuthenticationWithBearer(builder.Configuration);

// Register endpoints
builder.Services.AddEndpoints(typeof(Program).Assembly);

var app = builder.Build();

// CORS
app.UseCors("AllowLocalhost");

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty;
        c.OAuthClientId(builder.Configuration["JWT:ClientUrl"]);
        c.OAuthAppName("Swagger UI");
    });
}

app.UseHttpsRedirection();
app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

ApiVersionSet apiVersionSet = app.NewApiVersionSet()
    .HasApiVersion(new ApiVersion(1))
    .ReportApiVersions()
    .Build();

RouteGroupBuilder versionedGroup = app
    .MapGroup("api/v{version:apiVersion}")
    .WithApiVersionSet(apiVersionSet)
    .HasApiVersion(1);


app.MapEndpoints();

app.Run();

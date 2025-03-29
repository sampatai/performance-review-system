using Microsoft.OpenApi.Models;

namespace OfficePerformanceReview.WebAPI.DependencyExtensions
{
    public static class SwaggerGenExtension
    {
        internal static IServiceCollection AddSwaggerGenWithAuth(
           this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(o =>
            {
                o.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
                o.CustomSchemaIds(id => id.FullName!.Replace('+', '-'));
                o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter JWT Bearer token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT"

                });
                var securityRequirement = new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                           Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                        },
                        []
                    },

                };
                o.AddSecurityRequirement(securityRequirement);
            });


            return services;
        }
    }
}


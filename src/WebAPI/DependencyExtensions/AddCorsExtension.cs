using Microsoft.Extensions.Options;
using OfficePerformanceReview.Application.Common.Model.ConfigurationOption;

namespace OfficePerformanceReview.WebAPI.DependencyExtensions
{
    public static class AddCorsExtension
    {
        internal static IServiceCollection AddCorsPolicy(
        this IServiceCollection services, IConfiguration configuration)
        {
            // Register CorsSettings for DI and bind from configuration
            services.Configure<CorsSettings>(
                configuration.GetSection("CorsSettings"));

            services.AddCors(options =>
            {
                options.AddPolicy("AllowLocalhost", policyBuilder =>
                {
                    // Use a factory to resolve CorsSettings at runtime
                    policyBuilder.SetIsOriginAllowedToAllowWildcardSubdomains();
                    policyBuilder.AllowAnyMethod();
                    policyBuilder.AllowAnyHeader();
                    policyBuilder.AllowCredentials();
                    policyBuilder.SetIsOriginAllowed((origin) =>
                    {
                        // Resolve CorsSettings from the DI container at runtime
                        using var scope = services.BuildServiceProvider().CreateScope();
                        var corsSettings = scope.ServiceProvider.GetRequiredService<IOptions<CorsSettings>>().Value;
                        return corsSettings.AllowedOrigins.Contains(origin, StringComparer.OrdinalIgnoreCase);
                    });
                });
            });
            return services;
        }
    }
}

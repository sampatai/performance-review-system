using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using OfficePerformanceReview.Application.Common.Options;
using OfficePerformanceReview.Application.Common.Service;
using System.Text;

namespace OfficePerformanceReview.WebAPI.DependencyExtensions
{
    public static class AuthenticationExtension
    {
        internal static IServiceCollection AddAuthenticationWithBearer(
         this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AwsConfigurationOptions>(configuration.GetSection("AWS"));
            services.Configure<JwtOptions>(configuration.GetSection("JWT"));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(async (o) =>
            {
                var serviceProvider = services.BuildServiceProvider();
                var awsSecretService = serviceProvider.GetRequiredService<IAwsSecretService>();
                var jwtOptions = configuration.GetSection("JWT").Get<JwtOptions>() ?? new JwtOptions();
                var awsOptions = configuration.GetSection("AWS").Get<AwsConfigurationOptions>() ?? new AwsConfigurationOptions();

                var jwtSigningKey = await awsSecretService.GetSecretStringAsync(awsOptions.SecretsManager.SecretName);

                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSigningKey)
                    )
                };
            });
            return services;
        }
    }
}

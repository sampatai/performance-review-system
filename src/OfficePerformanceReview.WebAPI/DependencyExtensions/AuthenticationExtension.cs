using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace OfficePerformanceReview.WebAPI.DependencyExtensions
{
    public static class AuthenticationExtension
    {
        internal static IServiceCollection AddAuthenticationWithBearer(
         this IServiceCollection services, IConfiguration configuration)
        {
            var keyVaultUrl = configuration["AzureKeyVault:VaultUrl"];
            var client = new SecretClient(new Uri(keyVaultUrl!), new DefaultAzureCredential());
            var jwtKeySecret = client.GetSecret(configuration["AzureKeyVault:Secret"]);
            var jwtSigningKey = jwtKeySecret.Value.Value;
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["JWT:Issuer"],
                    ValidAudience = configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSigningKey))
                };

            });
            return services;
        }
    }
}

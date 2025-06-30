
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OfficePerformanceReview.Application.Common.Options;
using OfficePerformanceReview.Application.Common.Repository;
using OfficePerformanceReview.Domain.Profile.ValueObjects;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;


namespace OfficePerformanceReview.Application.Common.Service
{
    public interface IJWTService
    {
        RefreshToken CreateRefreshToken();
        Task<string> CreateJWT(Staff user);

    }
    public class JWTService(
            IReadonlyStaffRepository staffRepository,
            ILogger<JWTService> logger,
            IAwsSecretService awsSecretService,
            IOptions<AWSConfigurationOptions> awsOption,
            IOptions<JwtOptions> jwtOptions
        ) : IJWTService
    {
        public async Task<string> CreateJWT(Staff user)
        {
            try
            {
                var userClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.UserName!),
                    new Claim(ClaimTypes.GivenName, user.FirstName),
                    new Claim(ClaimTypes.Surname, user.LastName)
                };
                var roles = await staffRepository.GetRolesAsync(user);
                userClaims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
                string jwtSecret = await _GetJwtSecretFromKeyVault();
                SymmetricSecurityKey _jwtKey = new(Encoding.UTF8.GetBytes(jwtSecret));
                var credentials = new SigningCredentials(_jwtKey, SecurityAlgorithms.HmacSha512Signature);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(userClaims),
                    Expires = DateTime.UtcNow.AddMinutes(jwtOptions.Value.ExpiresInMinutes),
                    SigningCredentials = credentials,
                    Issuer = jwtOptions.Value.Issuer,
                    Audience = jwtOptions.Value.Audience
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var jwt = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(jwt);

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to retrieve JWT secret from Azure Key Vault");

                throw;
            }
        }

        public RefreshToken CreateRefreshToken()
        {
            try
            {
                var tokenBytes = new byte[64];
                using var rng = RandomNumberGenerator.Create();
                rng.GetBytes(tokenBytes);

                return new RefreshToken(Convert.ToBase64String(tokenBytes), DateTime.UtcNow.AddDays(jwtOptions.Value.RefreshTokenExpiresInDays));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "faild");
                throw;
            }
        }

        private async Task<string> _GetJwtSecretFromKeyVault()
        {
            try
            {

                return await awsSecretService.GetSecretStringAsync(awsOption.Value.SecretsManager.SecretName);
            }
            catch (Exception ex)

            {
                logger.LogError(ex, "Failed to retrieve JWT secret from Azure Key Vault");
                throw;
            }
        }


    }
}



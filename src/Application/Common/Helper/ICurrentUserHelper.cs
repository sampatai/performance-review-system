
using Microsoft.AspNetCore.Http;
using OfficeReview.Domain.Profile.Enums;
using System.Security.Claims;

namespace OfficePerformanceReview.Application.Common.Helper
{
    public interface ICurrentUserHelper
    {
        Task<CurrentUserInfo> GetCurrentUser();
        
    }
    public class HttpContextCurrentUserHelper(ILogger<HttpContextCurrentUserHelper> logger,
        IHttpContextAccessor httpContextAccessor) : ICurrentUserHelper
    {
        public async Task<CurrentUserInfo> GetCurrentUser()
        {
            try
            {
                var user = httpContextAccessor.HttpContext?.User;

                if (user == null || !user.Identity?.IsAuthenticated == true)
                {
                    return new CurrentUserInfo(0, "", "", new Role(0, ""));
                }

                var userIdString = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var email = user.FindFirst(ClaimTypes.Email)?.Value ?? "";
                var firstName = user.FindFirst(ClaimTypes.GivenName)?.Value ?? "";
                var lastName = user.FindFirst(ClaimTypes.Surname)?.Value ?? "";
                var fullName = $"{firstName} {lastName}".Trim();

                // Defensive: Validate and parse userId
                long userId = long.TryParse(userIdString, out var parsedId) ? parsedId : 0;

                // Get first role from claims
                var roleClaim = user.FindFirst(ClaimTypes.Role)?.Value;

                Role role = new Role(0, "");
                if (!string.IsNullOrEmpty(roleClaim) && int.TryParse(roleClaim, out var roleId))
                {
                    role = new Role(roleId, Role.FromValue<Role>(roleId).Name);
                }

                return new CurrentUserInfo(userId, email, fullName, role);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to retrieve current user from HttpContext.");
                throw new ApplicationException("Unable to retrieve current user.", ex);
            }
        }

    }
}

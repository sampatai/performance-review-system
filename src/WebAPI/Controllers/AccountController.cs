using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using OfficePerformanceReview.Application.CQRS.Command.User;
using OfficePerformanceReview.Application.Common.Model;
using OfficePerformanceReview.Application.CQRS.Query.Login;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace OfficePerformanceReview.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController(ISender sender,
        ILogger<AccountController> logger) : ControllerBase
    {

        [HttpPost("register")]
        [ProducesResponseType(typeof(RegisterUserModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Register(RegisterUserModel model, CancellationToken cancellationToken)
        {

            try
            {
                await sender.Send(new RegisterUser.Command(model), cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "@{model}", model);
                throw;
            }

        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginUserModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [AllowAnonymous]
        public async Task<ActionResult<LoginUserModel>> Login(LoginModel model, CancellationToken cancellationToken)
        {

            try
            {
                var result = await sender.Send(new LoginAccount.Query(model), cancellationToken);
                if (result.Unauthorized)
                    return Unauthorized(result.Message);
                CookiesOption(result.JWT, result.DateExpiresUtc.GetValueOrDefault());
                return Ok(new
                {
                    FirstName = result.FirstName,
                    LastName = result.LastName,
                    JWT = result.JWT,
                });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "@{model}", model);
                throw;
            }

        }

        [HttpPost("refresh-token")]
        [ProducesResponseType(typeof(LoginUserModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]

        public async Task<ActionResult<LoginUserModel>> RefereshToken(CancellationToken cancellationToken)
        {

            try
            {
                var token = Request.Cookies["reviewRefreshToken"];
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var result = await sender.Send(new ChekRefreshToken.Query(token, userId), cancellationToken);
                if (result.Unauthorized)
                    return Unauthorized(result.Message);
                CookiesOption(result.JWT, result.DateExpiresUtc.GetValueOrDefault());
                return Ok(new
                {
                    FirstName = result.FirstName,
                    LastName = result.LastName,
                    JWT = result.JWT,
                });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "faild");
                throw;
            }

        }

        #region Helper
        private void CookiesOption(string token, DateTime expiresDate)
        {
            var cookieOptions = new CookieOptions
            {
                Expires = expiresDate,
                IsEssential = true,
                HttpOnly = true,
                // Secure = true,          // Only sent over HTTPS
                SameSite = SameSiteMode.Strict //Prevent CSRF attacks
            };

            Response.Cookies.Append("reviewRefreshToken", token, cookieOptions);
        }
        #endregion

    }
}

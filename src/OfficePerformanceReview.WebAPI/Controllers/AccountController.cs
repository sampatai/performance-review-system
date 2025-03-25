using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using OfficePerformanceReview.Application.Model;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Threading;
using OfficePerformanceReview.Application.CQRS.Command.User;

namespace OfficePerformanceReview.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
                await sender.Send(new RegisterUserCommand.Command(model), cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "@{model}", model);
                throw;
            }

        }


    }
}

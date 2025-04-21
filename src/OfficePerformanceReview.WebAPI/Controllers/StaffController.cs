using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficePerformanceReview.Application.Common.Model;
using OfficePerformanceReview.Application.CQRS.Query.User;
using OfficePerformanceReview.WebAPI.DTOs;
using Swashbuckle.AspNetCore.Annotations;


namespace OfficePerformanceReview.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StaffController(ISender sender,
        ILogger<StaffController> logger) : ControllerBase
    {
        [HttpPost]
        [Route("users")]
        [SwaggerOperation(
             Summary = "user list",
        Description = "user list",
      OperationId = "performance.staff.search",
      Tags = new[] { "users" })]
        [SwaggerResponse(StatusCodes.Status200OK, "Returns a list of user", type: typeof(UserList))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Application failed to process the request")]
        public async Task<ActionResult<UserList>> GetUsers([FromBody, SwaggerParameter("Search params", Required = true)] FilterBase filter,
                                                                CancellationToken cancellationToken)
        {
            try
            {
                var command =new  ListUser.Query(filter);
                var result = await sender.Send(command, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "{@filter}", filter);
                throw;
            }
        }

        [HttpGet("{staffGuid}")]     
        [SwaggerOperation(
            Summary = "",
       Description = "get by user",
     OperationId = "performance.staff.edit",
     Tags = new[] { "users" })]
        [SwaggerResponse(StatusCodes.Status200OK, "Returns a user", type: typeof(EditUserModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Application failed to process the request")]
        public async Task<ActionResult<EditUserModel>> GetAsync(Guid staffGuid, CancellationToken cancellationToken)
        {
            try
            {
                var command = new GetByUser.Query(staffGuid);
                var result = await sender.Send(command, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "{@staffGuid}", staffGuid);
                throw;
            }
        }
    }
}


using OfficePerformanceReview.Application.Repository;
using OfficeReview.Domain.Profile.Enums;
using OfficeReview.Shared.SeedWork;

namespace OfficePerformanceReview.Application.CQRS.Command.User
{
    public sealed class Handler(ILogger<Handler> logger,
    IStaffRepository staffRepository) : IRequestHandler<RegisterUserCommand.Command>

    {
        public async Task Handle(RegisterUserCommand.Command request, CancellationToken cancellationToken)
        {
            try
            {
                Staff userToAdd = new(Enumeration.FromValue<Team>(request.Team),
                    request.FirstName,
                    request.LastName,
                    request.Email);

                var result = await staffRepository.CreateAsync(userToAdd, "Password@1");
                if (result.Succeeded)
                    await staffRepository.AddToRoleAsync(userToAdd, Enumeration.FromValue<Role>(request.Role).Name);
                else
                    throw new Exception(string.Join(",", result.Errors.Select(x=>x.Description)));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "{@request}", request);
                throw;
            }
        }
    }
}

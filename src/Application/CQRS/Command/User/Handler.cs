﻿using OfficeReview.Domain.Profile.Enums;
using OfficeReview.Shared.SeedWork;

namespace OfficePerformanceReview.Application.CQRS.Command.User
{
    public sealed class Handler(ILogger<Handler> logger,
    IStaffRepository staffRepository) : IRequestHandler<RegisterUser.Command>, IRequestHandler<UserUpdate.Command>

    {
        public async Task Handle(RegisterUser.Command request, CancellationToken cancellationToken)
        {
            try
            {
                Staff userToAdd = new(Enumeration.FromValue<Team>(request.Team),
                    request.FirstName,
                    request.LastName,
                    request.Email,
                    request.ManagerId);

                var result = await staffRepository.CreateAsync(userToAdd, "Password@1");
                if (result.Succeeded)
                    await staffRepository.AddToRoleAsync(userToAdd, Enumeration.FromValue<Role>(Convert.ToInt32(request.Role)).Name);
                else
                    throw new Exception(string.Join(",", result.Errors.Select(x => x.Description)));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "{@request}", request);
                throw;
            }
        }

        public async Task Handle(UserUpdate.Command request, CancellationToken cancellationToken)
        {
            try
            {
                Staff userToUpdate = await staffRepository.FindByIdAsync(request.StaffGuid, cancellationToken);
                userToUpdate.SetStaff(Enumeration.FromValue<Team>(request.Team),
                    request.FirstName,
                    request.LastName,
                    request.ManagerId);

                var result = await staffRepository.UpdateAsync(userToUpdate);
                string role = Enumeration.FromValue<Role>(Convert.ToInt32(request.Role)).Name;
                await staffRepository.RemoveRoleAsync(userToUpdate.Id.ToString(), role);
                await staffRepository.AddToRoleAsync(userToUpdate, role);

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "{@request}", request);
                throw;
            }
        }
    }
}

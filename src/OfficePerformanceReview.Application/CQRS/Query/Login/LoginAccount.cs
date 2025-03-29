using OfficePerformanceReview.Application.Common.Repository;
using OfficePerformanceReview.Application.Common.Service;

namespace OfficePerformanceReview.Application.CQRS.Query.Login
{
    public static class LoginAccount
    {
        #region Command/Query
        public record Query : LoginModel, IRequest<LoginResponse>
        {
            public Query(LoginModel original) : base(original)
            {
            }
        }
        #endregion
        #region Validation
        public sealed class Validator : AbstractValidator<Query>
        {
            public Validator()
            {
                RuleFor(x => x.UserName)
                     .NotNull()
                     .NotEmpty()
                     .WithMessage("Username is required");

                RuleFor(x => x.Password)
                    .NotNull()
                    .NotEmpty()
                    .WithMessage("Password is required");

            }
        }
        #endregion
        #region Handler
        public sealed class Handler(IReadonlyStaffRepository readonlyStaffRepository,
            IStaffRepository staffRepository,
            ILogger<Handler> logger,
            IJWTService jwtService) : IRequestHandler<Query, LoginResponse>

        {
            public const int _MAXIMUMLOGINATTEMPTS = 3;
            public async Task<LoginResponse> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var user = await readonlyStaffRepository
                        .FindByNameAsync(request.UserName);
                    if (user is null)
                        return new LoginResponse(
                            "Invalid username or password", false);
                    var result = await readonlyStaffRepository
                        .CheckPasswordSignInAsync(user, request.Password, false, cancellationToken);
                    if (result.IsLockedOut)
                        return new LoginResponse(string.Format("Your account has been locked. You should wait until {0} (UTC time) to be able to login", user.LockoutEnd),
                            false);
                    if (!result.Succeeded)
                    {
                        await staffRepository.AccessFailedAsync(user, cancellationToken);
                        if (user.AccessFailedCount >= _MAXIMUMLOGINATTEMPTS)
                        {
                            await staffRepository.SetLockoutEndDateAsync(user, DateTime.UtcNow.AddDays(1), cancellationToken);

                            return new LoginResponse(
                           string.Format("Your account has been locked. You should wait until {0} (UTC time) to be able to login", user.LockoutEnd),
                           false);
                        }
                        return new LoginResponse(
                           "Invalid username or password", false);
                    }
                    await staffRepository
                        .ResetAccessFailedCountAsync(user, cancellationToken);
                    await staffRepository
                        .SetLockoutEndDateAsync(user, null, cancellationToken);
                    var refreshToken = jwtService.CreateRefreshToken();
                    var token = await jwtService.CreateJWT(user);
                    user.SetRefereshToken(refreshToken.Token, refreshToken.DateExpiresUtc);
                    await staffRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
                    return new LoginResponse("Login Successfull", false, user.FirstName, user.LastName, token, refreshToken.DateExpiresUtc);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "{@request}", request);
                    throw;

                }
            }
        }
        #endregion

    }
}

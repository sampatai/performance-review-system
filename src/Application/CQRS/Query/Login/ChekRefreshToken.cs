

namespace OfficePerformanceReview.Application.CQRS.Query.Login
{
    public static class ChekRefreshToken
    {
        #region Command/Query
        public record Query(string Token, string UserId) : IRequest<LoginResponse> { }
        #endregion
        #region Validation
        public sealed class Validator : AbstractValidator<Query>
        {
            public Validator()
            {
                RuleFor(x => x.UserId)
                     .NotNull()
                     .NotEmpty()
                     .WithMessage("Invalid or expired token, please try to login");

                RuleFor(x => x.Token)
                    .NotNull()
                    .NotEmpty()
                    .WithMessage("Invalid or expired token, please try to login");

            }
        }
        #endregion
        #region Handler
        public sealed class Handler(IReadonlyStaffRepository readonlyStaffRepository,
            IStaffRepository staffRepository,
            ILogger<Handler> logger,
            IJWTService jwtService
             ) : IRequestHandler<Query, LoginResponse>

        {
            public async Task<LoginResponse> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var staff = await readonlyStaffRepository.FindByIdAsync(request.UserId);
                    var tokens = staff?.RefreshTokens.SingleOrDefault();
                    if (staff is null)
                        return new LoginResponse("Invalid or expired token, please try to login", false);
                    else if (tokens!.IsExpired)
                        return new LoginResponse("Invalid or expired token, please try to login", false);
                    else
                    {
                        var refreshToken = jwtService.CreateRefreshToken();
                        staff.SetRefereshToken(refreshToken.Token, refreshToken.DateExpiresUtc);
                        await staffRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
                        return new LoginResponse("valid", tokens.IsExpired, staff.FirstName, staff.LastName, refreshToken.Token, refreshToken.DateExpiresUtc);
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "@{request}", request);
                    throw;
                }
            }
        }
        #endregion
    }

}

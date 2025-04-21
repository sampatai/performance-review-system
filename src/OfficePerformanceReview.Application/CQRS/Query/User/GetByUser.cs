
namespace OfficePerformanceReview.Application.CQRS.Query.User
{
    public static class GetByUser
    {
        #region Query
        public record Query(Guid StaffGuid) : IRequest<EditUserModel> { }
        #endregion
        #region Validator
        public sealed class Validator : AbstractValidator<Query>
        {
            public Validator(IReadonlyStaffRepository readonlyStaffRepository)
            {
                RuleFor(x => x.StaffGuid)
                .MustAsync(readonlyStaffRepository.CheckUserExistsAsync)
                .WithMessage("Email already exists.");
            }
        }
        #endregion
        public class Handler(IReadonlyStaffRepository readonlyStaffRepository,
            ILogger<Handler> logger) : IRequestHandler<Query, EditUserModel>
        {
            public async Task<EditUserModel> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                   return await readonlyStaffRepository.FindByIdAsync(request.StaffGuid,cancellationToken);                    
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "{@request}", request);
                    throw;
                }
            }
        }
    }
}

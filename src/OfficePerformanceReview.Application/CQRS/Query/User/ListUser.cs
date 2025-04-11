namespace OfficePerformanceReview.Application.CQRS.Query.User
{
    public static class ListUser
    {
        #region Query
        public record Query : FilterBase, IRequest<UserList>
        {
            public Query(FilterBase original) : base(original)
            {
            }
        }
        #endregion

        #region Validator
        public sealed class Validator : FilterValidatorBase<Query> { }
        #endregion
        #region Handler
        public class Handler(IReadonlyStaffRepository readonlyStaffRepository,

            ILogger<Handler> logger) : IRequestHandler<Query, UserList>
        {
            public async Task<UserList> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var staff = await readonlyStaffRepository.GetStaffAsync(request, cancellationToken);
                    return new UserList
                    {
                        Data = staff.users,
                        TotalRecords = staff.totalCount
                    };
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "{@request}", request);
                    throw;
                }
            }
        }
    }
    #endregion

}


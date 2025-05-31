

namespace OfficePerformanceReview.Application.CQRS.Query.User
{
    public static class GetManagers
    {
        #region Query
        public record Query(int TeamId) : IRequest<IEnumerable<Managers>>;
        #endregion
        #region Handler
        public sealed class Handler(IReadonlyStaffRepository readonlyStaffRepository,
            ILogger<Handler> logger) : IRequestHandler<Query, IEnumerable<Managers>>
        {

            public async Task<IEnumerable<Managers>> Handle(Query request, CancellationToken cancellationToken)
            {

                try
                {
                    return await readonlyStaffRepository.GetManagersAsync(request.TeamId, cancellationToken);
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

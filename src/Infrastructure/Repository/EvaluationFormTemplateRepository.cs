
namespace OfficePerformanceReview.Infrastructure.Repository
{
    internal class EvaluationFormTemplateRepository(PerformanceReviewDbContext performanceReviewDbContext,
    ILogger<EvaluationFormTemplateRepository> logger
    ) : IEvaluationFormTemplateRepository
    {
        public IUnitOfWork UnitOfWork => performanceReviewDbContext;
    }
    internal class ReadonlyEvaluationFormTemplateRepository(PerformanceReviewDbContext performanceReviewDbContext,
    ILogger<ReadonlyEvaluationFormTemplateRepository> logger) : IReadonlyEvaluationFormTemplateRepository
    {

    }
}

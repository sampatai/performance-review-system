
using OfficePerformanceReview.Application.Common.Model;
using OfficeReview.Domain.Questions.Root;

namespace OfficePerformanceReview.Infrastructure.Repository
{
    internal class EvaluationFormTemplateRepository(PerformanceReviewDbContext performanceReviewDbContext,
    ILogger<EvaluationFormTemplateRepository> logger
    ) : IEvaluationFormTemplateRepository
    {
        public IUnitOfWork UnitOfWork => performanceReviewDbContext;

        public async Task<EvaluationFormTemplate> CreateAsync(EvaluationFormTemplate template, CancellationToken cancellationToken)
        {
            try
            {
                if (template.IsTransient())
                {
                    var entityEntry = await performanceReviewDbContext
                                     .EvaluationFormTemplates
                                     .AddAsync(template, cancellationToken);

                    return entityEntry.Entity;
                }
                else return template;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "{@template}", template);

                throw;
            }
        }

        public async Task<EvaluationFormTemplate> GetAsync(Guid evaluationFormTemplateGuid, CancellationToken cancellationToken)
        {
            try
            {
                return await performanceReviewDbContext
                    .EvaluationFormTemplates
                    .Include(x => x.Questions)
                    .AsSplitQuery()
                    .AsNoTracking()
                    .SingleOrDefaultAsync(x => x.EvaluationFormGuid == evaluationFormTemplateGuid, cancellationToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, " {evaluationFormTemplateGuid}", evaluationFormTemplateGuid);

                throw;
            }
        }

        public async Task<EvaluationFormTemplate> UpdateAsync(EvaluationFormTemplate template, CancellationToken cancellationToken)
        {
            try
            {

                return performanceReviewDbContext
                    .EvaluationFormTemplates
                    .Update(template)
                    .Entity;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Update {template}", template);
                throw;
            }
        }
    }
    internal class ReadonlyEvaluationFormTemplateRepository(PerformanceReviewDbContext performanceReviewDbContext,
    ILogger<ReadonlyEvaluationFormTemplateRepository> logger)
        : IReadonlyEvaluationFormTemplateRepository
    {
        public Task<(IEnumerable<EvaluationFormTemplate> Items, int TotalCount)> GetAllAsync(FilterBase filter, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

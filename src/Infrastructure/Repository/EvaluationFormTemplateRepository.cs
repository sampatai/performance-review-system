
using OfficePerformanceReview.Application.Common.Model;
using OfficePerformanceReview.Infrastructure.Extension;
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
                    .AsTracking()
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
        public async Task<bool> Exists(Guid evaluationFormTemplateGuid, CancellationToken cancellationToken)
        {
            try
            {

                return await performanceReviewDbContext
                    .EvaluationFormTemplates                   
                    .AnyAsync(x => x.EvaluationFormGuid == evaluationFormTemplateGuid, cancellationToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exists {evaluationFormTemplateGuid}", evaluationFormTemplateGuid);
                throw;
            }
        }

        public async Task<(IEnumerable<EvaluationFormTemplate> Items, int TotalCount)> GetAllAsync(FilterBase filter, CancellationToken cancellationToken)
        {
            try
            {
                var query = performanceReviewDbContext.EvaluationFormTemplates
                    .Include(x => x.Questions)
                    .Where(x => x.IsDeleted == false);

                var likeSearchTerm = $"%{filter.SearchTerm}%";

                query = query.Where(x => EF.Functions.Like(x.Name, likeSearchTerm) ||
                                         EF.Functions.Like(x.EvaluationType.Name, likeSearchTerm));

                int totalRecords = await query.CountAsync(cancellationToken);

                string sortColumn = GetSortColumnMap()
                    .Where(x => x.Key.Equals(filter.SortColumn))
                    .FirstOrDefault()
                    .Value;
                var results = await query
                   .ApplySorting(sortColumn, filter.SortDirection)
                  .Skip((filter.Page - 1) * filter.PageSize)
                  .Take(filter.PageSize)
                  .ToListAsync(cancellationToken);
                return (results, totalRecords);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "GetStaffAsync {filter}", filter);
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
                    .SingleOrDefaultAsync(x => x.EvaluationFormGuid == evaluationFormTemplateGuid, cancellationToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, " {evaluationFormTemplateGuid}", evaluationFormTemplateGuid);

                throw;
            }
        }

        private Dictionary<string, string> GetSortColumnMap()
        {
            return new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
           {
            { "name", "Name" },
           { "Evaluation", "EvaluationType.Name" }
          };
        }
    }
}

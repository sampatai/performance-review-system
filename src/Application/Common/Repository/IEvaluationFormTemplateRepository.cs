using OfficeReview.Domain.Questions.Root;
using OfficeReview.Shared.SeedWork;

namespace OfficePerformanceReview.Application.Common.Repository
{
    public interface IReadonlyEvaluationFormTemplateRepository : IReadOnlyRepository<EvaluationFormTemplate>
    {

    }
    public interface IEvaluationFormTemplateRepository : IRepository<EvaluationFormTemplate>
    {
        Task<EvaluationFormTemplate> CreateAsync(EvaluationFormTemplate template, CancellationToken cancellationToken);
        Task<EvaluationFormTemplate> UpdateAsync(EvaluationFormTemplate template, CancellationToken cancellationToken);
        Task<EvaluationFormTemplate> GetAsync(Guid evaluationFormTemplateGuid, CancellationToken cancellationToken);
    }
}

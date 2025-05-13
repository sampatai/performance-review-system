using OfficePerformanceReview.Domain.Questions.Enum;
using OfficeReview.Domain.Questions.Entities;
using OfficeReview.Domain.Questions.Enum;
using OfficeReview.Domain.Questions.Root;

namespace OfficePerformanceReview.Application.CQRS.Command.EvaluationForm
{
    public sealed class Handler(ILogger<Handler> logger,
      IEvaluationFormTemplateRepository evaluationFormTemplateRepository)
        : IRequestHandler<CreateEvaluationForm.Command>
    {
        public async Task Handle(CreateEvaluationForm.Command request,
            CancellationToken cancellationToken)
        {
            try
            {
                EvaluationFormTemplate evaluationForm = new(
                        request.Name,
                        new FormEvaluation(request.FormEvaluation.Id, request.FormEvaluation.Name)
                       );
                evaluationForm.AddQuestion(request.Questions.Select(x => new Question(
                    x.Question,
                    new QuestionType(x.QuestionType.Id, x.QuestionType.Name),
                    x.IsRequired)));
                await evaluationFormTemplateRepository.CreateAsync(evaluationForm, cancellationToken);
                await evaluationFormTemplateRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "{@request}", request);
                throw;
            }
        }
    }
}

using OfficePerformanceReview.Application.CQRS.Command.SetEvaluationForm;
using OfficePerformanceReview.Domain.Questions.Enum;
using OfficePerformanceReview.Domain.Questions.ValueObjects;
using OfficeReview.Domain.Questions.Entities;
using OfficeReview.Domain.Questions.Enum;
using OfficeReview.Domain.Questions.Root;

namespace OfficePerformanceReview.Application.CQRS.Command.EvaluationForm
{
    public sealed class Handler(ILogger<Handler> logger,
      IEvaluationFormTemplateRepository evaluationFormTemplateRepository)
        : IRequestHandler<CreateEvaluationForm.Command>, IRequestHandler<UpdateEvaluationForm.Command>
    {
        public async Task Handle(CreateEvaluationForm.Command request,
            CancellationToken cancellationToken)
        {
            try
            {
                EvaluationFormTemplate evaluationForm = new(
                        request.Name,
                        new FormEvaluation((int)request.FormEvaluation.Id, request.FormEvaluation.Name)
                       );
                evaluationForm.AddQuestion(request.Questions.Select(x => new Question(
                    x.Question,
                    new QuestionType((int)x.QuestionType.Id, x.QuestionType.Name),
                    x.IsRequired,
                    x.AddRemarks,
                    x.Options?.Select(x => new QuestionOption(x.Option)),
                    x.QuestionType.Id == QuestionType.RatingScale.Id ? x.RatingMin : null,
                    x.QuestionType.Id == QuestionType.RatingScale.Id ? x.RatingMax : null
                    )));
                await evaluationFormTemplateRepository.CreateAsync(evaluationForm, cancellationToken);
                await evaluationFormTemplateRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "{@request}", request);
                throw;
            }
        }

        public async Task Handle(UpdateEvaluationForm.Command request, CancellationToken cancellationToken)
        {
            try
            {
                var evaluationForm = await evaluationFormTemplateRepository.GetAsync(request.EvaluationFormGuid, cancellationToken);
                evaluationForm.SetEvaluationForm(request.Name, new FormEvaluation((int)request.FormEvaluation.Id, request.FormEvaluation.Name));
                evaluationForm.AddQuestion(request.Questions.Where(x => x.QuestionGuid.Equals(Guid.Empty)).Select(x => new Question(
                    x.Question,
                    new QuestionType((int)x.QuestionType.Id, x.QuestionType.Name),
                    x.IsRequired, x.AddRemarks)));


                foreach (var question in request.Questions)
                {
                    int? ratingMin = question.QuestionType.Id == QuestionType.RatingScale.Id ? question.RatingMin : null;
                    int? ratingMax = question.QuestionType.Id == QuestionType.RatingScale.Id ? question.RatingMax : null;
                    evaluationForm.SetQuestion(
                        question.QuestionGuid,
                        question.Question,
                        new QuestionType((int)question.QuestionType.Id, question.QuestionType.Name),
                        question.IsRequired,
                        question.AddRemarks,
                        question.Options?.Select(x => new QuestionOption(x.Option)),
                        ratingMin,
                        ratingMax
                    );
                }

                await evaluationFormTemplateRepository.UpdateAsync(evaluationForm, cancellationToken);
                await evaluationFormTemplateRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "{@request}", request);
                throw;
            }
        }
    }
}

using OfficePerformanceReview.Application.CQRS.Command.SetEvaluationForm;
using OfficePerformanceReview.Domain.Questions.Enum;
using OfficePerformanceReview.Domain.Questions.ValueObjects;
using OfficeReview.Domain.Questions.Entities;
using OfficeReview.Domain.Questions.Enum;
using OfficeReview.Domain.Questions.Root;
using OfficeReview.Shared.SeedWork;

namespace OfficePerformanceReview.Application.CQRS.Command.EvaluationForm
{
    public sealed class Handler(ILogger<Handler> logger,
          IEvaluationFormTemplateRepository evaluationFormTemplateRepository)
            : IRequestHandler<CreateEvaluationForm.Command>, IRequestHandler<UpdateEvaluationForm.Command>
    {
        public async Task Handle(CreateEvaluationForm.Command request, CancellationToken cancellationToken)
        {
            try
            {
                var formEvaluationGetByvalue = Enumeration.FromValue<FormEvaluation>(request.FormEvaluation);

                var evaluationForm = new EvaluationFormTemplate(request.Name, formEvaluationGetByvalue);

                var questions = request.Questions.Select(q =>
                {
                    var questionType = Enumeration.FromValue<QuestionType>(q.QuestionType);
                    int? ratingMin = questionType.Id == QuestionType.RatingScale.Id ? q.RatingMin : null;
                    int? ratingMax = questionType.Id == QuestionType.RatingScale.Id ? q.RatingMax : null;
                    var options = q.Options?.Select(opt => new QuestionOption(opt.Option));

                    return new Question(
                        q.Question,
                        new QuestionType(questionType.Id, questionType.Name),
                        q.IsRequired,
                        q.AddRemarks,
                        options,
                        ratingMin,
                        ratingMax
                    );
                });

                evaluationForm.AddQuestion(questions);
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

                var formEvaluationGetByvalue = Enumeration.FromValue<FormEvaluation>(request.FormEvaluation);

                evaluationForm.SetEvaluationForm(request.Name, formEvaluationGetByvalue);

                request.Questions.ToList().ForEach(q =>
                {
                    var questionType = Enumeration.FromValue<QuestionType>(q.QuestionType);
                    int? ratingMin = questionType.Id == QuestionType.RatingScale.Id ? q.RatingMin : null;
                    int? ratingMax = questionType.Id == QuestionType.RatingScale.Id ? q.RatingMax : null;
                    var options = q.Options?.Select(opt => new QuestionOption(opt.Option));

                    evaluationForm.SetQuestion(
                        q.QuestionGuid,
                        q.Question,
                        new QuestionType(questionType.Id, questionType.Name),
                        q.IsRequired,
                        q.AddRemarks,
                        options,
                        ratingMin,
                        ratingMax
                    );
                });


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

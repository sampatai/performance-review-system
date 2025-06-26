namespace OfficePerformanceReview.Application.CQRS.Query.GetEvaluationForm
{
    public static class GetEvaluationForm
    {
        #region Query
        public record Query(Guid EvaluationGuid) : IRequest<GetEvaluationFormDTO> { }
        #endregion

        #region Validator
        public sealed class Validator : AbstractValidator<Query>
        {
            public Validator(IReadonlyEvaluationFormTemplateRepository readonlyEvaluationFormTemplateRepository)
            {
                RuleFor(x => x.EvaluationGuid)
                   .MustAsync(readonlyEvaluationFormTemplateRepository.Exists)
                   .WithMessage("Form template not exists.");
            }
        }
        #endregion

        public sealed class Handler : IRequestHandler<Query, GetEvaluationFormDTO>
        {
            private readonly IReadonlyEvaluationFormTemplateRepository _readonlyEvaluationFormTemplateRepository;
            private readonly ILogger<Handler> _logger;

            public Handler(IReadonlyEvaluationFormTemplateRepository readonlyEvaluationFormTemplateRepository,
                ILogger<Handler> logger)
            {
                _readonlyEvaluationFormTemplateRepository = readonlyEvaluationFormTemplateRepository;
                _logger = logger;
            }

            public async Task<GetEvaluationFormDTO> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    _logger.LogInformation("Handling GetEvaluationForm query for EvaluationGuid: {EvaluationGuid}",
                    request.EvaluationGuid);

                    var evaluationFormTemplate = await _readonlyEvaluationFormTemplateRepository
                        .GetAsync(request.EvaluationGuid, cancellationToken);

                    return new GetEvaluationFormDTO
                    (
                        EvaluationFormGuid: evaluationFormTemplate.EvaluationFormGuid,
                        Name: evaluationFormTemplate.Name,
                        FormEvaluation: new(evaluationFormTemplate.EvaluationType.Id, evaluationFormTemplate.Name),
                        Questions: evaluationFormTemplate.Questions.Select(q => new GetQuestionDTO(
                            QuestionGuid: q.QuestionGuid,
                            Question: q.QuestionText,
                            QuestionType: new(q.QuestionType.Id, q.QuestionType.Name),
                            IsRequired: q.IsRequired,
                            AddRemarks: q.AddRemarks,
                            Options: q.Options?.Select(o => new OptionsDTO(o.OptionText)),
                            RatingMin: q.RatingMin,
                            RatingMax: q.RatingMax
                        ))
                    );
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "{@request}", request);
                    throw;
                }
                
            }
        }
    }
}

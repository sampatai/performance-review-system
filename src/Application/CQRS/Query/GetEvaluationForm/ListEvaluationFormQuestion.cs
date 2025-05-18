namespace OfficePerformanceReview.Application.CQRS.Query.GetEvaluationForm
{
    public static class ListEvaluationFormQuestion
    {
        #region Query
        public record Query : FilterBase, IRequest<EvaluationFormListDTO>
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
        public sealed class Handler(ILogger<Handler> logger,
            IReadonlyEvaluationFormTemplateRepository evaluationFormTemplateRepository) : IRequestHandler<Query, EvaluationFormListDTO>
        {
            public async Task<EvaluationFormListDTO> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var result = await evaluationFormTemplateRepository.GetAllAsync(request, cancellationToken);
                    return new EvaluationFormListDTO
                    {
                        Data = result.Items.Select(x => new GetEvaluationFormDTO(
                            x.Name,
                           new NameValue(x.EvaluationType.Id, x.EvaluationType.Name),
                            x.Questions.Select(a => new GetQuestionDTO(a.QuestionText, new NameValue(a.QuestionType.Id, a.QuestionType.Name),
                            a.IsRequired, a.QuestionGuid, a.Options.Select(x => new OptionsDTO (x.OptionText)), a.RatingMin, a.RatingMax)),
                            x.EvaluationFormGuid)
                        ),
                        TotalRecords = result.TotalCount
                    };
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

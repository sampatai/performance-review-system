using OfficePerformanceReview.Application.Common.Model.EvaluationForm;

namespace OfficePerformanceReview.Application.CQRS.Query.GetEvaluationForm
{
    public static class ListEvaluationFormQuestion
    {
        #region Query
        public record Query : FilterBase, IRequest, IRequest<EvaluationFormListDTO>
        {
            protected Query(FilterBase original) : base(original)
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
                throw new NotImplementedException();
            }
        }
        #endregion
    }

}

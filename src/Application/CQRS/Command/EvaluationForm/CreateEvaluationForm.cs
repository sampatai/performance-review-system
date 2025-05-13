using OfficePerformanceReview.Application.Common.Model.EvaluationForm;
namespace OfficePerformanceReview.Application.CQRS.Command.EvaluationForm
{
    public static class CreateEvaluationForm
    {
        #region Command/Query
        public sealed record Command : CreateEvaluationFormDTO, IRequest
        {
            public Command(CreateEvaluationFormDTO original) : base(original)
            {
            }
        }
        #endregion
        #region Validation
        public sealed class Validator : ValidatorBase<EvaluationFormDTO> { }
        #endregion

    }
}

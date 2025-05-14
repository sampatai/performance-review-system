using OfficePerformanceReview.Application.CQRS.Command.EvaluationForm;

namespace OfficePerformanceReview.Application.CQRS.Command.SetEvaluationForm
{
    public static class UpdateEvaluationForm
    {
        #region Command/Query
        public sealed record Command : UpdateEvaluationFormDTO, IRequest
        {
            public Command(UpdateEvaluationFormDTO original) : base(original)
            {
            }
        }
        #endregion

        #region Validation
        public sealed class Validator : ValidatorBase<UpdateEvaluationFormDTO>
        {
            public Validator(IReadonlyEvaluationFormTemplateRepository readonlyEvaluationFormTemplateRepository)
            {
                RuleFor(x => x.EvaluationFormGuid)
                    .MustAsync(readonlyEvaluationFormTemplateRepository.Exists)
                    .WithMessage("Form not exists.");
                RuleForEach(x => x.Questions)
                         .SetValidator(new QuestionValidator<GetQuestionDTO>());

                
            }
        }
        #endregion

    }
}

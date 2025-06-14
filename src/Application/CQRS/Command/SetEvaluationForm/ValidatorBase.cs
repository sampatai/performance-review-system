using OfficePerformanceReview.Domain.Questions.Enum;
using OfficeReview.Domain.Questions.Enum;
using OfficeReview.Shared.SeedWork;


namespace OfficePerformanceReview.Application.CQRS.Command.EvaluationForm
{
    public abstract class ValidatorBase<T> : AbstractValidator<T> where T : EvaluationFormDTO
    {
        protected ValidatorBase()
        {
            RuleFor(r => r.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("Evaluation form name is required");

            RuleFor(x => x.FormEvaluation)
                .Must(x => Enumeration.GetAll<FormEvaluation>().Any(a => a.Id == x))
                .WithMessage("Invalid form evaluation.");

        }
    }
    public class QuestionValidator<T> : AbstractValidator<T> where T : QuestionDTO
    {
        public QuestionValidator()
        {
            RuleFor(x => x.Question)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .WithName("Question");

            RuleFor(x => x.QuestionType)
                .Must(x => Enumeration.GetAll<QuestionType>().Any(a => a.Id == x))
                .WithMessage("Invalid question type.");
        }
    }


}

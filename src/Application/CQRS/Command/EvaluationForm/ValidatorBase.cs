using OfficePerformanceReview.Application.Common.Model.EvaluationForm;
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
                .Must(x => Enumeration.GetAll<FormEvaluation>().Any(a => a.Id == x.Id))
                .WithMessage("Invalid form evaluation.");

            RuleForEach(x => x.Questions)
             .ChildRules(q =>
             {
                 q.RuleFor(x => x.Question)
                         .Cascade(CascadeMode.Stop)
                         .NotNull()
                         .NotEmpty()
                         .WithMessage("{PropertyName} is required.")
                         .WithName("Question");
                 q.RuleFor(x => x.QuestionType)
                          .Must(x => Enumeration.GetAll<QuestionType>().Any(a => a.Id == x.Id))
                          .WithMessage("Invalid question type.");

             });

            RuleFor(x => x.Questions)
             .Must((m, q) =>
             {
                 var duplicates = m.Questions.GroupBy(x => x.Question)
                                           .Where(g => g.Count() > 1)
                                           .Select(y => y.Key)
                                           .ToList();
                 return !duplicates.Any();
             })
             .WithMessage("question must be unique.");
        }
    }
}

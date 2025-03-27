using OfficeReview.Domain.Profile.Enums;
using OfficeReview.Shared.SeedWork;


namespace OfficePerformanceReview.Application.CQRS.Command.User
{
    public abstract class ValidatorBase<T> : AbstractValidator<T> where T : RegisterUserModel
    {
        protected ValidatorBase()
        {
            RuleFor(r => r.FirstName)
           .NotEmpty()
           .WithMessage("FirstName is required.");

            RuleFor(r => r.LastName)
            .NotEmpty()
            .WithMessage("LastName is required.");

            RuleFor(r => r.Email)
             .NotEmpty()
             .WithMessage("Email is required.")
             .EmailAddress()
             .WithMessage("Invalid email format.");

             RuleFor(x => x.Team)
              .Must(x => Enumeration.GetAll<Team>().Any(a => a.Id == x))
              .WithMessage("Invalid Teams.");
        }
    }
}

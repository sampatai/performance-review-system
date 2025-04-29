using OfficePerformanceReview.Application.Common.Helper;

namespace OfficePerformanceReview.Application.Common.Validators
{
    public abstract class FilterValidatorBase<TQuery> : AbstractValidator<TQuery> where TQuery : FilterBase
    {
        protected FilterValidatorBase()
        {
            RuleFor(x => x.Page)
                   .GreaterThanOrEqualTo(1)
                   .WithMessage("Invalid {PropertyName} (> 0)")
                   .WithName("page number");

            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Invalid {PropertyName} (> 0)")
                .WithName("page size");

            RuleFor(x => x.SearchTerm)
             .MinimumLength(3)
             .When(x => !string.IsNullOrWhiteSpace(x.SearchTerm))
             .WithMessage("Please provide at least 3 letters to initiate a search.");

            RuleFor(x => x.SortDirection)
                .Must(dir => dir.ToLower() == SortEnum.Asc.Name || dir.ToLower() == SortEnum.Desc.Name)
                .When(x => !string.IsNullOrWhiteSpace(x.SortDirection))
                .WithMessage("Sort direction must be 'asc' or 'desc'")
                .WithName("sort direction");
        }
    }
}

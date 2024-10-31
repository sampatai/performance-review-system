using OfficePerformanceReview.Domain.Common.ValueObjects;
using OfficePerformanceReview.Domain.PerformanceReview.Entities;
using OfficePerformanceReview.Domain.PerformanceReview.Enums;
using OfficeReview.Domain.Questions.Entities;

namespace OfficePerformanceReview.Domain.PerformanceReview.Root
{
    public class PerformanceReviewOverview : AuditableEntity, IAggregateRoot
    {
        private List<NameValue> _Reviewer = new();

        protected PerformanceReviewOverview()
        {

        }
        public Guid ReviewGuid { get; private set; }
        public DateRange ReviewDate { get; private set; }
        public string Year { get; private set; }
        public Session ReviewSession { get; private set; }
        public IReadOnlyList<NameValue> Reviewer => _Reviewer.AsReadOnly();

    }
}

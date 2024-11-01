using OfficePerformanceReview.Domain.Common.ValueObjects;
using OfficePerformanceReview.Domain.PerformanceReview.Enums;
namespace OfficePerformanceReview.Domain.PerformanceReview.Root
{
    public class PerformanceReviewOverview : AuditableEntity, IAggregateRoot
    {
        private List<NameValue> _FeedBackGivers = new();

        protected PerformanceReviewOverview()
        {

        }
        public Guid ReviewGuid { get; private set; }
        public DateRange ReviewDate { get; private set; }
        public string Year { get; private set; }
        public Session ReviewSession { get; private set; }
        public IReadOnlyList<NameValue> FeedBackGivers => _FeedBackGivers.AsReadOnly();

        public PerformanceReviewOverview(DateRange reviewDate, string year, Session session)
        {
            Guard.Against.NullOrEmpty(year, nameof(year));
            Guard.Against.Null(reviewDate, nameof(reviewDate));
            Guard.Against.Null(session, nameof(session));
            this.ReviewGuid = Guid.NewGuid();
            this.ReviewSession = session;
            this.Year = year;
            this.ReviewDate = reviewDate;
        }

        public void SetPerformanceReviewOverview(DateRange reviewDate, string year, Session session)
        {
            Guard.Against.NullOrEmpty(year, nameof(year));
            Guard.Against.Null(reviewDate, nameof(reviewDate));
            Guard.Against.Null(session, nameof(session));
            this.ReviewGuid = Guid.NewGuid();
            this.ReviewSession = session;
            this.Year = year;
            this.ReviewDate = reviewDate;
        }
        public void SetFeedBackGiver(List<NameValue> feedBackGivers)
        {
            _FeedBackGivers.Clear();
            _FeedBackGivers.AddRange(feedBackGivers);
        }

    }
}

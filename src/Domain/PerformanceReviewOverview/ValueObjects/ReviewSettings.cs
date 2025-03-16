namespace OfficePerformanceReview.Domain.PerformanceReviewOverview.ValueObjects
{
    public class ReviewSettings : ValueObject
    {
        public DateTime StartDate { get; private set; }
        public int DurationInDays { get; private set; }
        public bool AllowDuplicateReviews { get; private set; }

        private ReviewSettings() { }

        public ReviewSettings(DateTime startDate, int durationInDays, bool allowDuplicateReviews)
        {
            Guard.Against.Null(startDate, nameof(startDate));
            Guard.Against.OutOfRange(durationInDays, nameof(durationInDays), 1, 365);

            StartDate = startDate;
            DurationInDays = durationInDays;
            AllowDuplicateReviews = allowDuplicateReviews;
        }

        public DateTime GetEndDate() => StartDate.AddDays(DurationInDays);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return StartDate;
            yield return DurationInDays;
            yield return AllowDuplicateReviews;
        }
    }
}

namespace OfficePerformanceReview.Domain.PerformanceReviewFormAssign.ValueObjects
{
    public class ReviewCycle : ValueObject
    {
        public long ReviewCycleId { get; private set; }
        public string Name { get; private set; }
        protected ReviewCycle()
        {
            
        }
        public ReviewCycle(long reviewCycleId, string name)
        {
            Guard.Against.NullOrEmpty(name, nameof(name));
            Guard.Against.NegativeOrZero(reviewCycleId, nameof(reviewCycleId));
            ReviewCycleId = reviewCycleId;
            Name = name;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
            yield return ReviewCycleId;
        }
    }
}

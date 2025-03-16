namespace OfficePerformanceReview.Domain.PerformanceReviewOverview.Entities
{
    public class ReviewerAssignment : Entity
    {
        public long StaffId { get; private set; }
        public long ReviewerId { get; private set; }
        public DateTime AssignedDate { get; private set; }

        private ReviewerAssignment() { }

        public ReviewerAssignment(long staffId, long reviewerId)
        {
            Guard.Against.NegativeOrZero(staffId, nameof(staffId));
            Guard.Against.NegativeOrZero(reviewerId, nameof(reviewerId));

            StaffId = staffId;
            ReviewerId = reviewerId;
            AssignedDate = DateTime.UtcNow;
        }
    }
}

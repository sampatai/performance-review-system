namespace OfficePerformanceReview.Domain.PerformanceReviewFormAssign.Entities
{
    public class ReviewerAssignment : Entity
    {
        public Guid ReviewerAssignmentGuid { get; private set; }
        public long StaffId { get; private set; }
        public long ReviewerId { get; private set; }
        public DateTime DeadLine { get; private set; }
        public bool IsActive { get; private set; }
        public long PerformanceReviewTemplateId { get; private set; }

        protected ReviewerAssignment() { }

        internal ReviewerAssignment(long staffId,
            long reviewerId,
            DateTime deadLine,

            long performanceReviewTemplateId)
        {
            Guard.Against.NegativeOrZero(staffId, nameof(staffId));
            Guard.Against.NegativeOrZero(reviewerId, nameof(reviewerId));
            Guard.Against.OutOfRange(deadLine, nameof(deadLine), DateTime.MinValue, DateTime.MaxValue);

            StaffId = staffId;
            ReviewerId = reviewerId;
            DeadLine = deadLine;
            ReviewerAssignmentGuid = Guid.NewGuid();
            IsActive = true;
            PerformanceReviewTemplateId = performanceReviewTemplateId;
        }
        internal void SetReviewerAssignment(long staffId,
            long reviewerId,
            DateTime deadLine,
            long performanceReviewTemplateId)
        {
            Guard.Against.NegativeOrZero(staffId, nameof(staffId));
            Guard.Against.NegativeOrZero(reviewerId, nameof(reviewerId));
            Guard.Against.OutOfRange(deadLine, nameof(deadLine), DateTime.MinValue, DateTime.MaxValue);
            StaffId = staffId;
            ReviewerId = reviewerId;
            DeadLine = deadLine;
            PerformanceReviewTemplateId = performanceReviewTemplateId;
        }
    }
}

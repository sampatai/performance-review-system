using OfficePerformanceReview.Domain.Common.ValueObjects;
using OfficePerformanceReview.Domain.PerformanceReview.Entities;


namespace OfficePerformanceReview.Domain.PerformanceReview.Root
{
    public class PerformanceReview : AuditableEntity, IAggregateRoot
    {
        protected PerformanceReview() { }

        public NameValue ReviewerInitiator { get; private set; }
        public NameValue ReviewerOf { get; private set; }
        public Guid PerformanceReviewGuid { get; private set; }
        public int PerformanceOverviewId { get; private set; }
        public int EvaluationFormId { get; private set; }
        public DateTime? ReviewDate { get; private set; }

        public List<Reviewee> _Reviewees = new();
        public IReadOnlyList<Reviewee> Reviewees => _Reviewees.AsReadOnly();

        private List<Objective> _objectives = new();
        public IReadOnlyList<Objective> Objectives => _objectives.AsReadOnly();
        private Feedback _Feedback = new();
        public List<BehaviorMetric> BehaviorMetrics { get; private set; } = new List<BehaviorMetric>();

        public Feedback Feedbacks => _Feedback;
    }
}

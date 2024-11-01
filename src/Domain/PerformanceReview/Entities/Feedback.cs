using OfficePerformanceReview.Domain.PerformanceReview.Enums;

namespace OfficePerformanceReview.Domain.PerformanceReview.Entities
{
    public class Feedback : Entity
    {
        protected Feedback()
        {
            
        }
        public Guid FeedbackGuid { get; private set; }
        private List<BehaviorMetric> _BehaviorMetrics = new();
        public IReadOnlyList<BehaviorMetric> BehaviorMetrics => _BehaviorMetrics.AsReadOnly();
        public string? RevieweeComment { get; set; }
        public string? ReviewerComment { get; set; }
        public OverallRating? PotentialLevel { get; private set; }
        public FeedbackStatus RevieweeFeedbackStatus { get; private set; }
        public FeedbackStatus ReviewerFeedbackStatus { get; private set; }


    }
}

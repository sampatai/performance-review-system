using OfficePerformanceReview.Domain.Common.ValueObjects;
using OfficePerformanceReview.Domain.PerformanceReview.Enums;
using OfficeReview.Domain.Questions.Entities;

namespace OfficePerformanceReview.Domain.PerformanceReview.Entities
{
    public class Feedback : Entity
    {
        protected Feedback()
        {

        }
        public Guid FeedbackGuid { get; private set; }
        private List<BehaviorMetric> _behaviorMetrics = new();
        public IReadOnlyList<BehaviorMetric> BehaviorMetrics => _behaviorMetrics.AsReadOnly();
        public string? RevieweeComment { get; set; }
        public string? ReviewerComment { get; set; }
        public OverallRating? PotentialLevel { get; private set; }
        public FeedbackStatus RevieweeFeedbackStatus { get; private set; }
        public FeedbackStatus? ReviewerFeedbackStatus { get; private set; }

        public Feedback(string? revieweeComment, FeedbackStatus revieweeFeedbackStatus)
        {
            Guard.Against.Null(revieweeFeedbackStatus, nameof(revieweeFeedbackStatus));

            FeedbackGuid = Guid.NewGuid();
            RevieweeComment = revieweeComment;
            RevieweeFeedbackStatus = revieweeFeedbackStatus;

        }

        internal void AddBehaviorMetricByReviewee(QuestionFeedback question,
            RatingScale ratingScale,
            string revieweeRemarks)
        {
            Guard.Against.Null(question, nameof(question));

            _behaviorMetrics.Add(new BehaviorMetric(question, ratingScale, revieweeRemarks));
        }
        internal void SetBehaviorMetricByReviewee(Guid metricGUID,
           RatingScale ratingScale,
           string revieweeRemarks)
        {
            var single = _behaviorMetrics.SingleOrDefault(x => x.MetricGUID == metricGUID)!;
            single.SetBehaviorMetric(ratingScale, revieweeRemarks);
        }

        internal void SetBehaviorMetricByReviewer(Guid metricGUID,
           RatingScale ratingScale,
           string reviewerRemarks)
        {
            var single = _behaviorMetrics.SingleOrDefault(x => x.MetricGUID == metricGUID)!;
            single.SetReviewerRemarks(reviewerRemarks, ratingScale);
        }
        internal void SetReviewer(FeedbackStatus feedbackStatus,
            string reviewerComment,
            OverallRating rating)
        {
            Guard.Against.Null(feedbackStatus, nameof(feedbackStatus));
            Guard.Against.Null(rating, nameof(rating));

            ReviewerFeedbackStatus = feedbackStatus;
            PotentialLevel = rating;
            ReviewerComment = reviewerComment;
        }
        internal void SetReviewee(string revieweeComment, FeedbackStatus feedbackStatus)
        {
            Guard.Against.Null(feedbackStatus, nameof(feedbackStatus));

            RevieweeFeedbackStatus = feedbackStatus;
            RevieweeComment = revieweeComment;
        }
    }
}

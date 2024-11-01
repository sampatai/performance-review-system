using OfficePerformanceReview.Domain.Common.ValueObjects;
using OfficePerformanceReview.Domain.PerformanceReview.Enums;
namespace OfficePerformanceReview.Domain.PerformanceReview.Entities
{
    public class BehaviorMetric : Entity
    {
        protected BehaviorMetric()
        {

        }
        public Guid MetricGUID { get; private set; }
        public QuestionFeedback Metric { get; private set; }
        public RatingScale RevieweeRating { get; set; }
        public string RevieweeRemarks { get; set; }
        public RatingScale? ReviewerRating { get; set; }
        public string? ReviewerRemarks { get; set; }

        public BehaviorMetric(QuestionFeedback question,
            RatingScale ratingScale,
            string revieweeRemarks)
        {
            Guard.Against.Null(question, nameof(question));
            Guard.Against.Null(ratingScale, nameof(ratingScale));
            Guard.Against.NullOrEmpty(revieweeRemarks, nameof(revieweeRemarks));
            Metric = question;
            RevieweeRating = ratingScale;
            RevieweeRemarks = revieweeRemarks;

        }

        internal void SetReviewerRemarks(string reviewerRemarks, RatingScale ratingScale)
        {
            Guard.Against.Null(ratingScale, nameof(ratingScale));
            Guard.Against.NullOrEmpty(reviewerRemarks, nameof(reviewerRemarks));
            ReviewerRating = ratingScale;
            RevieweeRemarks = reviewerRemarks;
        }
        internal void SetBehaviorMetric(
            RatingScale ratingScale,
            string revieweeRemarks)
        {
            Guard.Against.Null(ratingScale, nameof(ratingScale));
            Guard.Against.NullOrEmpty(revieweeRemarks, nameof(revieweeRemarks));
            RevieweeRating = ratingScale;
            RevieweeRemarks = revieweeRemarks;
        }
    }
}

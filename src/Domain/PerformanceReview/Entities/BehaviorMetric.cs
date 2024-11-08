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
        public string EmployeeRemarks { get; set; }
        public RatingScale? ManagerRating { get; set; }
        public string? ManagerRemarks { get; set; }

        public BehaviorMetric(QuestionFeedback question,
            RatingScale ratingScale,
            string employeeRemarks)
        {
            Guard.Against.Null(question, nameof(question));
            Guard.Against.Null(ratingScale, nameof(ratingScale));
            Guard.Against.NullOrEmpty(employeeRemarks, nameof(employeeRemarks));
            Metric = question;
            RevieweeRating = ratingScale;
            EmployeeRemarks = employeeRemarks;

        }

        internal void SetManagerRemarks(string managerRemarks, RatingScale ratingScale)
        {
            Guard.Against.Null(ratingScale, nameof(ratingScale));
            Guard.Against.NullOrEmpty(managerRemarks, nameof(managerRemarks));
            ManagerRating = ratingScale;
            EmployeeRemarks = managerRemarks;
        }
        internal void SetBehaviorMetric(
            RatingScale ratingScale,
            string employeeRemarks)
        {
            Guard.Against.Null(ratingScale, nameof(ratingScale));
            Guard.Against.NullOrEmpty(employeeRemarks, nameof(employeeRemarks));
            RevieweeRating = ratingScale;
            EmployeeRemarks = employeeRemarks;
        }
    }
}

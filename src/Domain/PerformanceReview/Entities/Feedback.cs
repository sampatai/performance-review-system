﻿using OfficePerformanceReview.Domain.Common.ValueObjects;
using OfficePerformanceReview.Domain.PerformanceReview.Enums;

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
        public string? EmployeeComment { get; set; }
        public string? ReviewerComment { get; set; }
        public OverallRating? PotentialLevel { get; private set; }
        public FeedbackStatus EmployeeFeedbackStatus { get; private set; }
        public FeedbackStatus? ManagerFeedbackStatus { get; private set; }

        public Feedback(FeedbackStatus employeeFeedbackStatus)
        {
            Guard.Against.Null(employeeFeedbackStatus, nameof(employeeFeedbackStatus));
            FeedbackGuid = Guid.NewGuid();
            this.PotentialLevel = OverallRating.Reviewing;
            EmployeeFeedbackStatus = FeedbackStatus.Pending;
        }

        internal void AddBehaviorMetricByEmployee(QuestionFeedback question,
            RatingScale ratingScale,
            string employeeRemarks)
        {
            if (IsTransient())
            {
                Guard.Against.Null(question, nameof(question));

                _behaviorMetrics.Add(new BehaviorMetric(question, ratingScale, employeeRemarks));
            }
        }
        internal void SetBehaviorMetricByEmployee(Guid metricGUID,
           RatingScale ratingScale,
           string employeeRemarks)
        {
            var single = _behaviorMetrics.SingleOrDefault(x => x.MetricGUID == metricGUID)!;
            single.SetBehaviorMetric(ratingScale, employeeRemarks);
        }

        internal void SetBehaviorMetricByManager(Guid metricGUID,
           RatingScale ratingScale,
           string reviewerRemarks)
        {
            var single = _behaviorMetrics.SingleOrDefault(x => x.MetricGUID == metricGUID)!;
            single.SetManagerRemarks(reviewerRemarks, ratingScale);
        }
        internal void SetManager(FeedbackStatus feedbackStatus,
            string reviewerComment,
            OverallRating rating)
        {
            Guard.Against.Null(feedbackStatus, nameof(feedbackStatus));
            Guard.Against.Null(rating, nameof(rating));

            ManagerFeedbackStatus = feedbackStatus;
            PotentialLevel = rating;
            ReviewerComment = reviewerComment;
        }
        internal void SetEmployee(string? employeeComment, FeedbackStatus feedbackStatus)
        {
            Guard.Against.Null(feedbackStatus, nameof(feedbackStatus));

            EmployeeFeedbackStatus = feedbackStatus;
            EmployeeComment = employeeComment;
        }

    }
}

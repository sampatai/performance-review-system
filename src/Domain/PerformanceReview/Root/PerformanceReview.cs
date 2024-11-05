using OfficePerformanceReview.Domain.Common.ValueObjects;
using OfficePerformanceReview.Domain.PerformanceReview.Entities;
using OfficePerformanceReview.Domain.PerformanceReview.Enums;
using OfficeReview.Domain.Questions.Enum;
using OfficeReview.Shared.Exceptions;



namespace OfficePerformanceReview.Domain.PerformanceReview.Root
{
    public class PerformanceReview : AuditableEntity, IAggregateRoot
    {
        protected PerformanceReview() { }

        public NameValue Reviewer { get; private set; }
        public NameValue ReviewOf { get; private set; }
        public Guid PerformanceReviewGuid { get; private set; }
        public int PerformanceOverviewId { get; private set; }
        public FormEvaluation EvaluationType { get; private set; }
        public DateTime ReviewDate { get; private set; }
        public FeedbackStatus FeedbackStatus { get; private set; }

        public List<Reviewee> _Reviewees = new();
        public IReadOnlyList<Reviewee> Reviewees => _Reviewees.AsReadOnly();
        public Feedback Feedbacks { get; private set; }

        private List<Objective> _objectives = new();
        public IReadOnlyList<Objective> Objectives => _objectives.AsReadOnly();


        public PerformanceReview(NameValue reviewer,
            NameValue reviewOf,
            DateTime reviewDate,
            int performanceOverviewId)
        {
            Guard.Against.Null(reviewer);
            Guard.Against.Null(reviewOf);
            Guard.Against.Null(reviewDate);
            Guard.Against.NegativeOrZero(performanceOverviewId);

            this.Reviewer = reviewer;
            this.ReviewOf = reviewOf;
            this.ReviewDate = reviewDate;
            this.EvaluationType = FormEvaluation.SelfManagerEvaluation;
            FeedbackStatus = FeedbackStatus.Pending;
            PerformanceOverviewId = performanceOverviewId;
            PerformanceReviewGuid = Guid.NewGuid();
            this.Feedbacks = new Feedback(FeedbackStatus.Pending);
        }

        public void SetPerformanceReview(
            NameValue reviewOf,
           DateTime reviewDate
           )
        {
            Guard.Against.Null(reviewDate);
            Guard.Against.Null(reviewOf);
            this.ReviewDate = reviewDate;
            this.ReviewOf = reviewOf;
        }

        #region reviewee state change
        public void AddReviewees(int reviewById, string reviewBy, DateOnly deadLine)
        {
            _Reviewees.Add(new Reviewee(reviewById, reviewBy, deadLine));
        }

        public void SetReviewee(Guid revieweeGuid, int reviewById, string reviewBy, DateOnly deadLine)
        {
            var reviewee = _ValidateReviewee(revieweeGuid);
            if (reviewee == null)
                throw new OfficeReviewDomainException("Reviewee not assign");
            reviewee.SetReviewee(reviewById, reviewBy, deadLine);
        }

        public void SetFeedback(Guid revieweeGuid, IEnumerable<QuestionFeedback> feedbacks)
        {
            var reviewee = _ValidateReviewee(revieweeGuid);
            reviewee.SetFeedback(feedbacks);
        }
        #endregion
        #region Feedback state change

        public void AddBehaviorMetricByReviewee(QuestionFeedback question,
            RatingScale ratingScale,
            string revieweeRemarks)
        {

            Feedbacks.AddBehaviorMetricByReviewee(question, ratingScale, revieweeRemarks);
        }
        public void SetBehaviorMetricByReviewee(Guid metricGUID,
           RatingScale ratingScale,
           string revieweeRemarks)
        {
            this.Feedbacks.SetBehaviorMetricByReviewee(metricGUID, ratingScale, revieweeRemarks);
        }
        public void SetReviewee(string? revieweeComment, FeedbackStatus feedbackStatus)
        {
            this.Feedbacks.SetReviewee(revieweeComment, feedbackStatus);
        }
        public void SetBehaviorMetricByReviewer(Guid metricGUID,
           RatingScale ratingScale,
           string reviewerRemarks)
        {
            this.Feedbacks.SetBehaviorMetricByReviewer(metricGUID, ratingScale, reviewerRemarks);
        }
        public void SetReviewer(FeedbackStatus feedbackStatus,
          string reviewerComment,
          OverallRating rating)
        {
            this.Feedbacks.SetReviewer(feedbackStatus, reviewerComment, rating);
        }
        #endregion

        #region Objectives
        public void AddObjective(IEnumerable<Objective> objects)
        {
            _objectives.AddRange(objects);
        }
        public void SetObjective(IEnumerable<Objective> objects)
        {
            foreach (var objective in objects)
            {
                var single = _objectives.Single(o => o.ObjectiveGuid.Equals(objective.ObjectiveGuid));
                single.SetObjective(objective.Description, objective.ActionPlan, objective.Timeline, objective.ProgressStatus);
            }
        }
        public void RemoveObjective(IEnumerable<Guid> guids)
        {
            _objectives.RemoveAll(x => guids.Contains(x.ObjectiveGuid));
        }
        #endregion

        #region Helper
        private Reviewee _ValidateReviewee(Guid revieweeGuid)
        {
            var reviewee = _Reviewees.Single(x => x.RevieweeGuid == revieweeGuid);
            if (reviewee == null)
                throw new OfficeReviewDomainException("Reviewee not assign");
            return reviewee;
        }

        #endregion


    }
}

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

        public NameValue CompletedBy { get; private set; }
        public NameValue AppraisedName { get; private set; }
        public Guid PerformanceReviewGuid { get; private set; }
        public int PerformanceOverviewId { get; private set; }
        public FormEvaluation EvaluationType { get; private set; }
        public DateTime ReviewDate { get; private set; }
        public FeedbackStatus FeedbackStatus { get; private set; }

        public List<Reviewer> _Reviewers = new();
        public IReadOnlyList<Reviewer> Reviewers => _Reviewers.AsReadOnly();
        public Feedback Feedbacks { get; private set; }

        private List<Objective> _objectives = new();
        public IReadOnlyList<Objective> Objectives => _objectives.AsReadOnly();


        public PerformanceReview(NameValue completedBy,
            NameValue appraisedName,
            DateTime reviewDate,
            int performanceOverviewId)
        {
            Guard.Against.Null(completedBy);
            Guard.Against.Null(appraisedName);
            Guard.Against.Null(reviewDate);
            Guard.Against.NegativeOrZero(performanceOverviewId);

            this.CompletedBy = completedBy;
            this.AppraisedName = appraisedName;
            this.ReviewDate = reviewDate;
            this.EvaluationType = FormEvaluation.SelfManagerEvaluation;
            FeedbackStatus = FeedbackStatus.Pending;
            PerformanceOverviewId = performanceOverviewId;
            PerformanceReviewGuid = Guid.NewGuid();
            this.Feedbacks = new Feedback(FeedbackStatus.Pending);
        }

        public void SetPerformanceReview(
            NameValue appraisedName,
           DateTime reviewDate
           )
        {
            Guard.Against.Null(reviewDate);
            Guard.Against.Null(appraisedName);
            this.ReviewDate = reviewDate;
            this.AppraisedName = appraisedName;
        }

        #region reviewee state change
        public void AddReviewees(int completedById, string completedBy, DateOnly deadLine)
        {
            _Reviewers.Add(new Reviewer(completedById, completedBy, deadLine));
        }

        public void SetReviewee(Guid completedGuid, int completedById, string completedBy, DateOnly deadLine)
        {
            var reviewee = _ValidateReviewee(completedGuid);
            if (reviewee == null)
                throw new OfficeReviewDomainException("Reviewee not assign");
            reviewee.SetReviewer(completedById, completedBy, deadLine);
        }

        public void SetFeedback(Guid revieweeGuid, IEnumerable<QuestionFeedback> feedbacks)
        {
            var reviewee = _ValidateReviewee(revieweeGuid);
            reviewee.SetFeedback(feedbacks);
        }
        #endregion
        #region Feedback state change

        public void AddBehaviorMetricByEmployee(QuestionFeedback question,
            RatingScale ratingScale,
            string revieweeRemarks)
        {

            Feedbacks.AddBehaviorMetricByEmployee(question, ratingScale, revieweeRemarks);
        }
        public void SetBehaviorMetricByEmployee(Guid metricGUID,
           RatingScale ratingScale,
           string revieweeRemarks)
        {
            this.Feedbacks.SetBehaviorMetricByEmployee(metricGUID, ratingScale, revieweeRemarks);
        }
        public void SetEmployee(string? revieweeComment, FeedbackStatus feedbackStatus)
        {
            this.Feedbacks.SetEmployee(revieweeComment, feedbackStatus);
        }
        public void SetBehaviorMetricByManager(Guid metricGUID,
           RatingScale ratingScale,
           string reviewerRemarks)
        {
            this.Feedbacks.SetBehaviorMetricByManager(metricGUID, ratingScale, reviewerRemarks);
        }
        public void SetManager(FeedbackStatus feedbackStatus,
          string reviewerComment,
          OverallRating rating)
        {
            this.Feedbacks.SetManager(feedbackStatus, reviewerComment, rating);
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
        private Reviewer _ValidateReviewee(Guid reviewerGuid)
        {
            var reviewee = _Reviewers.Single(x => x.ReviewerGuid == reviewerGuid);
            if (reviewee == null)
                throw new OfficeReviewDomainException("Reviewer not assign");
            return reviewee;
        }

        #endregion


    }
}

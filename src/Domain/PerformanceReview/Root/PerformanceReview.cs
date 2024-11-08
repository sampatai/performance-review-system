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
        #region Helper
        private Reviewer _ValidateReviewee(Guid revieweeGuid)
        {
            var reviewee = _Reviewers.Single(x => x.ReviewerGuid == revieweeGuid);
            if (reviewee == null)
                throw new OfficeReviewDomainException("Reviewee not assign");
            return reviewee;
        }
        #endregion


    }
}

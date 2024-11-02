using OfficePerformanceReview.Domain.Common.ValueObjects;
using OfficePerformanceReview.Domain.PerformanceReview.Entities;
using OfficePerformanceReview.Domain.PerformanceReview.Enums;
using OfficeReview.Domain.Questions.Enum;
using OfficeReview.Shared.Exceptions;
using System.Collections.Generic;


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

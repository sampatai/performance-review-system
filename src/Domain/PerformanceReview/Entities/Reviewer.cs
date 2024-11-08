using OfficePerformanceReview.Domain.Common.ValueObjects;
using OfficePerformanceReview.Domain.PerformanceReview.Enums;
using OfficeReview.Domain.Questions.Enum;
namespace OfficePerformanceReview.Domain.PerformanceReview.Entities
{
    public class Reviewer : Entity
    {
        public Guid ReviewerGuid { get; private set; }
        public NameValue CompletedBy { get; private set; }
        public DateOnly? ReviewDate { get; private set; }
        public DateOnly DeadLine { get; private set; }
        public FormEvaluation EvaluationType { get; private set; }
        private List<QuestionFeedback> _FeedBacks = new();
        public IReadOnlyList<QuestionFeedback> Feedbacks => _FeedBacks.AsReadOnly();
        public FeedbackStatus FeedbackStatus { get; private set; }

        public bool IsActive { get; private set; }
        protected Reviewer() { }

        public Reviewer(int staffId, string name, DateOnly deadLine)
        {

            Guard.Against.Null(deadLine, nameof(deadLine));
            EvaluationType = FormEvaluation.PeerEvaluation;
            CompletedBy = new NameValue(staffId, name);
            FeedbackStatus = FeedbackStatus.Pending;
            IsActive = true;
            DeadLine = deadLine;
        }

        internal void SetReviewer(int staffId, string name, DateOnly reviewDate)
        {
            Guard.Against.Null(reviewDate, nameof(reviewDate));
            Guard.Against.OutOfRange(reviewDate, nameof(reviewDate), DateOnly.MinValue, DateOnly.MaxValue);
            ReviewDate = reviewDate;
            CompletedBy = new NameValue(staffId, name);

            _EvaluateFeedbackStatus();
        }

        internal void SetFeedback(IEnumerable<QuestionFeedback> feedbacks)
        {
            _FeedBacks.Clear();
            _FeedBacks.AddRange(feedbacks);
            _EvaluateFeedbackStatus();
        }

        #region Helper
        private void _EvaluateFeedbackStatus()
        {
            if (_FeedBacks.Any() && this.ReviewDate is not null)
                FeedbackStatus = FeedbackStatus.Completed;
        }
        #endregion
    }
}

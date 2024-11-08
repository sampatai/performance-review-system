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

        public List<PeerEvaluation> _evaluators = new();
        public IReadOnlyList<PeerEvaluation> Evaluators => _evaluators.AsReadOnly();
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

        #region employee state change
        public void AddPeerEvaluation(int completedById, string completedBy, DateOnly deadLine)
        {
            _evaluators.Add(new PeerEvaluation(completedById, completedBy, deadLine));
        }

        public void SetPeerEvaluation(Guid peerEvaluationGuid, int completedById, string completedBy, DateOnly deadLine)
        {
            var employee = _ValidatePeerEvaluation(peerEvaluationGuid);
            if (employee == null)
                throw new OfficeReviewDomainException("Evaluator not assign");
            employee.SetPeerEvaluation(completedById, completedBy, deadLine);
        }

        public void SetFeedback(Guid peerEvaluationGuid, IEnumerable<QuestionFeedback> feedbacks)
        {
            var employee = _ValidatePeerEvaluation(peerEvaluationGuid);
            employee.SetFeedback(feedbacks);
        }
        #endregion
        #region Feedback state change

        public void AddBehaviorMetricByEmployee(QuestionFeedback question,
            RatingScale ratingScale,
            string employeeRemarks)
        {

            Feedbacks.AddBehaviorMetricByEmployee(question, ratingScale, employeeRemarks);
        }
        public void SetBehaviorMetricByEmployee(Guid metricGUID,
           RatingScale ratingScale,
           string employeeRemarks)
        {
            this.Feedbacks.SetBehaviorMetricByEmployee(metricGUID, ratingScale, employeeRemarks);
        }
        public void SetEmployee(string? employeeComment, FeedbackStatus feedbackStatus)
        {
            this.Feedbacks.SetEmployee(employeeComment, feedbackStatus);
        }
        public void SetBehaviorMetricByManager(Guid metricGUID,
           RatingScale ratingScale,
           string managerRemarks)
        {
            this.Feedbacks.SetBehaviorMetricByManager(metricGUID, ratingScale, managerRemarks);
        }
        public void SetManager(FeedbackStatus feedbackStatus,
          string managerComment,
          OverallRating rating)
        {
            this.Feedbacks.SetManager(feedbackStatus, managerComment, rating);
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
        private PeerEvaluation _ValidatePeerEvaluation(Guid peerEvaluationGuid)
        {
            var employee = _evaluators.Single(x => x.PeerEvaluationGuid == peerEvaluationGuid);
            if (employee == null)
                throw new OfficeReviewDomainException("Evaluator not assign");
            return employee;
        }

        #endregion


    }
}

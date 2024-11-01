using OfficePerformanceReview.Domain.Common.ValueObjects;
using OfficePerformanceReview.Domain.PerformanceReview.Entities;
using OfficePerformanceReview.Domain.PerformanceReview.Enums;
using OfficeReview.Domain.Questions.Enum;
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
        public DateTime? ReviewDate { get; private set; }
        public FeedbackStatus FeedbackStatus { get; private set; }
        
        public List<Reviewee> _Reviewees = new();
        public IReadOnlyList<Reviewee> Reviewees => _Reviewees.AsReadOnly();

        private List<Objective> _objectives = new();
        public IReadOnlyList<Objective> Objectives => _objectives.AsReadOnly();
       
        public Feedback Feedbacks { get; private set; }
    }
}

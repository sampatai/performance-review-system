using OfficePerformanceReview.Domain.Common.ValueObjects;
using OfficePerformanceReview.Domain.PerformanceReview.Enums;

namespace OfficePerformanceReview.Domain.PerformanceReview.Entities
{
    public class Objective : Entity
    {
        public Guid ObjectiveGuid { get; private set; }
        public string Description { get; private set; }
        public string ActionPlan { get; private set; }
        public DateRange Timeline { get; private set; }
        public ObjectiveStatus ProgressStatus { get; set; }

        public Objective(string description,
            string actionPlan,
            DateRange timeline,
            ObjectiveStatus objectiveStatus)
        {
            Guard.Against.NullOrEmpty(description, nameof(description));
            Guard.Against.NullOrEmpty(actionPlan, nameof(actionPlan));
            Guard.Against.Null(timeline, nameof(timeline));
            Guard.Against.Null(objectiveStatus, nameof(objectiveStatus));
            ObjectiveGuid = Guid.NewGuid();
            Description = description;
            ActionPlan = actionPlan;
            Timeline = timeline;
            ProgressStatus = objectiveStatus;
        }

        internal void SetObjective(string description,
            string actionPlan,
            DateRange timeline,
            ObjectiveStatus objectiveStatus)
        {
            Guard.Against.NullOrEmpty(description, nameof(description));
            Guard.Against.NullOrEmpty(actionPlan, nameof(actionPlan));
            Guard.Against.Null(timeline, nameof(timeline));
            Guard.Against.Null(objectiveStatus, nameof(objectiveStatus));
            ObjectiveGuid = Guid.NewGuid();
            Description = description;
            ActionPlan = actionPlan;
            Timeline = timeline;
            ProgressStatus = objectiveStatus;
        }
    }

}

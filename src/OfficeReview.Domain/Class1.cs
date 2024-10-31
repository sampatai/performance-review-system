using System;
using System.Collections.Generic;
using System.Linq;

namespace PerformanceReviewSystem
{
    // ========== Value Objects ==========

    public class RatingScale
    {
        public static RatingScale Always => new RatingScale("Always");
        public static RatingScale Often => new RatingScale("Often");
        public static RatingScale Sometimes => new RatingScale("Sometimes");
        public static RatingScale Rarely => new RatingScale("Rarely");
        public static RatingScale Never => new RatingScale("Never");

        public string Value { get; private set; }

        private RatingScale(string value)
        {
            Value = value;
        }

        public override string ToString() => Value;
    }

    public class OverallRating
    {
        public static OverallRating RoughDiamond => new OverallRating("Rough Diamond");
        public static OverallRating RisingStar => new OverallRating("Rising Star");
        public static OverallRating FutureLeader => new OverallRating("Future Leader");
        public static OverallRating CorePerformer => new OverallRating("Core Performer");
        public static OverallRating HighPerformer => new OverallRating("High Performer");
        public static OverallRating SolidPerformer => new OverallRating("Solid Performer");
        public static OverallRating TechnicalExpert => new OverallRating("Technical Expert");

        public string Value { get; private set; }

        private OverallRating(string value)
        {
            Value = value;
        }

        public override string ToString() => Value;
    }

    public class ObjectiveStatus
    {
        public static ObjectiveStatus Incomplete => new ObjectiveStatus("Incomplete");
        public static ObjectiveStatus Started => new ObjectiveStatus("Started");
        public static ObjectiveStatus Complete => new ObjectiveStatus("Complete");

        public string Value { get; private set; }

        private ObjectiveStatus(string value)
        {
            Value = value;
        }

        public override string ToString() => Value;
    }

    // ========== Domain: Employee Profile ==========

    public class Employee
    {
        public Guid EmployeeID { get; private set; }
        public string Name { get; private set; }
        public string PositionTitle { get; private set; }
        public Guid ManagerID { get; private set; }
        public List<PerformanceReview> PerformanceReviews { get; private set; } = new List<PerformanceReview>();

        public Employee(string name, string positionTitle, Guid managerId)
        {
            EmployeeID = Guid.NewGuid();
            Name = name;
            PositionTitle = positionTitle;
            ManagerID = managerId;
        }
    }

   

    // ========== Domain: Performance Management ==========

    public class PerformanceReview
    {
        public Guid ReviewID { get; private set; }
        public DateTime ReviewDate { get; private set; }
        public Guid EmployeeID { get; private set; }
        public Guid ManagerID { get; private set; }
        public OverallRating PotentialLevel { get; set; }
        public List<Feedback> Feedbacks { get; private set; } = new List<Feedback>();
        public List<BehaviorMetric> BehaviorMetrics { get; private set; } = new List<BehaviorMetric>();

        public PerformanceReview(Guid employeeId, Guid managerId, DateTime reviewDate)
        {
            ReviewID = Guid.NewGuid();
            EmployeeID = employeeId;
            ManagerID = managerId;
            ReviewDate = reviewDate;
        }
    }

    public class Feedback
    {
        public Guid FeedbackID { get; private set; }
        public string EmployeeComment { get; set; }
        public string ManagerComment { get; set; }
        public BehaviorMetric LinkedMetric { get; private set; }

        public Feedback(BehaviorMetric linkedMetric)
        {
            FeedbackID = Guid.NewGuid();
            LinkedMetric = linkedMetric;
        }
    }

    public class BehaviorMetric
    {
        public Guid MetricID { get; private set; }
        public string MetricName { get; private set; }
        public RatingScale Rating { get; set; }
        public string EmployeeRemarks { get; set; }
        public string ManagerRemarks { get; set; }

        public BehaviorMetric(string metricName)
        {
            MetricID = Guid.NewGuid();
            MetricName = metricName;
        }
    }

    // ========== Domain: Goal and Objective Management ==========

    public class Objective
    {
        public Guid ObjectiveID { get; private set; }
        public string Description { get; private set; }
        public string ActionPlan { get; private set; }
        public DateTime Timeline { get; private set; }
        public ObjectiveStatus ProgressStatus { get; set; }

        public Objective(string description, string actionPlan, DateTime timeline)
        {
            ObjectiveID = Guid.NewGuid();
            Description = description;
            ActionPlan = actionPlan;
            Timeline = timeline;
            ProgressStatus = ObjectiveStatus.Incomplete;
        }
    }

    // Other domains and services remain the same as in the previous single-file implementation
}

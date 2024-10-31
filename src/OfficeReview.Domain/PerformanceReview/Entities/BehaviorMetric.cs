using OfficePerformanceReview.Domain.Common.ValueObjects;
using OfficePerformanceReview.Domain.PerformanceReview.Enums;


namespace OfficePerformanceReview.Domain.PerformanceReview.Entities
{
    public class BehaviorMetric : Entity
    {
        protected BehaviorMetric()
        {

        }
        public Guid MetricGUID { get; private set; }
        public string MetricName { get; private set; }
        public List<QuestionFeedback> _Metric = new();
        public IReadOnlyList<QuestionFeedback> Metric => _Metric.AsReadOnly();
        public RatingScale Rating { get; set; }
        public string EmployeeRemarks { get; set; }
        public string ManagerRemarks { get; set; }

        public BehaviorMetric(string metricName)
        {
            MetricName = metricName;
        }
    }
}

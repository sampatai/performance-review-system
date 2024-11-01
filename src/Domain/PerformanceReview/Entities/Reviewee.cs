using OfficePerformanceReview.Domain.Common.ValueObjects;
using OfficePerformanceReview.Domain.PerformanceReview.ValueObjects;

namespace OfficePerformanceReview.Domain.PerformanceReview.Entities
{
    public class Reviewee : Entity
    {
        public Guid RevieweeGuid { get; private set; }
        public NameValue ReviewOf { get; private set; }
        public NameValue ReviewBy { get; private set; }
        public DateTime? ReviewDate { get; private set; }
        public int EvaluationFormId { get; private set; }
        public List<QuestionFeedback> _FeedBacks = new();
        public IReadOnlyList<QuestionFeedback> Feedbacks => _FeedBacks.AsReadOnly();
        protected Reviewee() { }

        public Reviewee(int staffId, string name, int evaluationFormId)
        {
            EvaluationFormId = evaluationFormId;
            ReviewBy = new NameValue(staffId, name);
        }


    }
}

namespace OfficePerformanceReview.Domain.PerformanceReviewFormAssign.ValueObjects
{
    public class EvaluationFormTemplete : ValueObject
    {
        public long EvaluationFormTempleteId { get; private set; }
        public string Name { get; private set; }
       
        protected EvaluationFormTemplete() { }
        public EvaluationFormTemplete(string name, long evaluationFormTempleteId)
        {
            Guard.Against.NullOrEmpty(name, nameof(name));
            Guard.Against.NegativeOrZero(evaluationFormTempleteId, nameof(evaluationFormTempleteId));
            Name = name;
            EvaluationFormTempleteId = evaluationFormTempleteId;
            
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
            yield return EvaluationFormTempleteId;
        }
    }
}

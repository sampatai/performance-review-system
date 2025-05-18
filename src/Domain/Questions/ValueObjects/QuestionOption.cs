namespace OfficePerformanceReview.Domain.Questions.ValueObjects
{
    public class QuestionOption : ValueObject
    {
        public string OptionText { get; private set; }
        protected QuestionOption()
        {
            
        }
        public QuestionOption(string optionText)
        {
            Guard.Against.NullOrEmpty(optionText, nameof(optionText));
            OptionText = optionText;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return OptionText;
        }
    }
}

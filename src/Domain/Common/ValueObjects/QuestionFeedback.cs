using OfficeReview.Domain.Questions.Root;

namespace OfficePerformanceReview.Domain.Common.ValueObjects
{
    public class QuestionFeedback : ValueObject
    {
        public int QuestionId { get; private set; }
        public string QuestionText { get; private set; }
        public string Text { get; private set; }

        protected QuestionFeedback() { }

        public QuestionFeedback(int questionId, string questionText, string text)
        {
            Guard.Against.NegativeOrZero(questionId, nameof(questionId));

            QuestionId = questionId;
            QuestionText = Guard.Against.NullOrEmpty(questionText);
            Text = Guard.Against.NullOrEmpty(text);

        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return QuestionId;
            yield return QuestionText;
            yield return Text;
        }

    }
}

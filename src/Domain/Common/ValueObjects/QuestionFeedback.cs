using OfficePerformanceReview.Domain.PerformanceReview.Enums;
using OfficePerformanceReview.Domain.Questions.Enum;


namespace OfficePerformanceReview.Domain.Common.ValueObjects
{
    public class QuestionFeedback : ValueObject
    {
        public long QuestionId { get; private set; }
        public string QuestionText { get; private set; }
        public QuestionType QuestionType { get; private set; }
        public string Text { get; private set; }
        public RatingScale? RatingScale { get; private set; }
        protected QuestionFeedback() { }

        public QuestionFeedback(int questionId,
            string questionText, QuestionType questionType,  string text, RatingScale? ratingScale)
        {
            Guard.Against.NegativeOrZero(questionId, nameof(questionId));
            QuestionId = questionId;
            QuestionText = Guard.Against.NullOrEmpty(questionText);
            Text = Guard.Against.NullOrEmpty(text);
            this.RatingScale = ratingScale;
            this.QuestionType = questionType;
        }
       
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return QuestionId;
            yield return QuestionText;
            yield return Text;
            yield return RatingScale;
            yield return QuestionType;
        }

    }
}

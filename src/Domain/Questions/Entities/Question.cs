
using OfficePerformanceReview.Domain.Questions.Enum;

namespace OfficeReview.Domain.Questions.Entities
{
    public class Question : Entity
    {
        protected Question()
        {

        }
        public Guid QuestionGuid { get; private set; }
        public string QuestionText { get; private set; }
        public bool IsDeleted { get; private set; }
        public bool IsActive { get; private set; }
        public QuestionType QuestionType { get; private set; }
        public List<string> Options { get; private set; }  // Options for MultipleChoice or SingleChoice questions
        public bool IsRequired { get; private set; }  // Whether the question is required

        public Question(string question, QuestionType questionType, bool isRequired = false, List<string> options = null)
        {
            Guard.Against.NullOrEmpty(question, nameof(question));
            Guard.Against.Null(questionType, nameof(questionType));
            IsActive = true;
            IsDeleted = false;
            QuestionText = question;
            QuestionGuid = Guid.NewGuid();
            QuestionType = questionType;
            Options = options ?? new List<string>();  // Initialize with empty list if options are null
            IsRequired = isRequired;
            Validate();
        }
        internal void SetQuestion(string question, QuestionType questionType, List<string> options = null)
        {
            Guard.Against.NullOrEmpty(question, nameof(question));
            Guard.Against.Null(questionType, nameof(questionType));
            QuestionText = question;
            QuestionType = questionType;
            Options = options ?? new List<string>();
            Validate();
        }

        internal void SetDeActivate()
        {
            IsActive = false;
        }
        internal void SetDelete()
        {
            IsDeleted = true;
        }

        private void Validate()
        {
            switch (QuestionType)
            {
                case var q when q == QuestionType.Rating:
                    break;

                case var q when q == QuestionType.RatingAndText:
                    break;

                case var q when q == QuestionType.MultipleChoice || q == QuestionType.SingleChoice:
                    if (Options == null || Options.Count == 0)
                    {
                        throw new InvalidOperationException("Multiple Choice or Single Choice questions must have at least one option.");
                    }
                    break;

                case var q when q == QuestionType.Text:
                    break;

                default:
                    throw new InvalidOperationException("Invalid question type.");
            }
        }

    }
}

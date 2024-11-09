
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
        public Question(string question, QuestionType questionType)
        {
            Guard.Against.NullOrEmpty(question, nameof(question));
            Guard.Against.Null(questionType, nameof(questionType));
            IsActive = true;
            IsDeleted = false;
            QuestionText = question;
            QuestionGuid = Guid.NewGuid();

        }
        internal void SetQuestion(string question, QuestionType questionType)
        {
            Guard.Against.NullOrEmpty(question, nameof(question));
            Guard.Against.Null(questionType, nameof(questionType));
            QuestionText = question;
            QuestionType = questionType;
        }

        internal void SetDeActivate()
        {
            IsActive = false;
        }
        internal void SetDelete()
        {
            IsDeleted = true;
        }
    }
}

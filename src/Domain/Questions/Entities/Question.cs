
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

        public Question(string question)
        {
            Guard.Against.NullOrEmpty(question, nameof(question));
            IsActive = true;
            IsDeleted = false;
            QuestionText = question;
            QuestionGuid = Guid.NewGuid();

        }
        internal void SetQuestion(string question)
        {
            Guard.Against.NullOrEmpty(question, nameof(question));

            QuestionText = question;
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

using OfficeReview.Domain.Questions.Enum;

namespace OfficeReview.Domain.Questions.Entities
{
    public class Question : Entity
    {
        protected Question()
        {

        }
        public Guid QuestionGuid { get; private set; }
        public string QuestionText { get; private set; }
        public FeedbackQuestionType FeedbackQuestionType { get; private set; }
    }
}

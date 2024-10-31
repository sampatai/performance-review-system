namespace OfficeReview.Domain.Questions.Enum
{
    public class FeedbackQuestionType : Enumeration
    {

        public static FeedbackQuestionType Text = new(1, "Text");
        public static FeedbackQuestionType SingleSelect = new(2, "SingleSelect");
        public static FeedbackQuestionType Range = new(3, "Range");
        public static FeedbackQuestionType Matrix = new(4, "Matrix");
        public FeedbackQuestionType(int id, string name) : base(id, name)
        {
        }
    }

}

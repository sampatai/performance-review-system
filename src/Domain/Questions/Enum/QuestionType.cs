namespace OfficePerformanceReview.Domain.Questions.Enum
{
    public class QuestionType : Enumeration
    {
        public static QuestionType Text = new(1, nameof(Text));
        public static QuestionType RatingAndText = new(2, "Rating And Text");
        public static QuestionType SingleChoice = new (3, "Single Choice");
        public static QuestionType RatingScale = new(4, "Rating Scale");
        public static QuestionType MultipleChoice = new(5, "Multiple Choice");
        public QuestionType(int id, string name) : base(id, name)
        {
        }
    }
    
}

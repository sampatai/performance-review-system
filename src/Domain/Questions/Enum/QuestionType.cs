namespace OfficePerformanceReview.Domain.Questions.Enum
{
    public class QuestionType : Enumeration
    {
        public static QuestionType Text = new(1, nameof(Text));
        public static QuestionType RatingScale = new(2, "Rating Scale");
        public static QuestionType SingleChoice = new (3, "Single Choice");
        public static QuestionType MultipleChoice = new(4, "Multiple Choice");
        public QuestionType(int id, string name) : base(id, name)
        {
        }
    }
    
}

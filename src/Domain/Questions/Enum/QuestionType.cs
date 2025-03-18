namespace OfficePerformanceReview.Domain.Questions.Enum
{
    public class QuestionType : Enumeration
    {
        public static QuestionType Text = new(1, nameof(Text));
        public static QuestionType RatingAndText = new(2, "Rating And Text");
        public static QuestionType Rating = new(3, "Rating");
        public QuestionType(int id, string name) : base(id, name)
        {
        }
    }
    
}

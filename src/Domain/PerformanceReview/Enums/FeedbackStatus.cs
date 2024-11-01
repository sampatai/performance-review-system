namespace OfficePerformanceReview.Domain.PerformanceReview.Enums
{
    public class FeedbackStatus : Enumeration
    {
        public static readonly FeedbackStatus Pending = new FeedbackStatus(1, "Pending");
        public static readonly FeedbackStatus Completed = new FeedbackStatus(2, "Completed");
     
        public FeedbackStatus(int id, string name) : base(id, name)
        {
        }
    }
}

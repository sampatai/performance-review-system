namespace OfficePerformanceReview.Domain.PerformanceReview.Enums
{
    public class RatingScale : Enumeration
    {
        public static RatingScale Always = new(1, "Always");
        public static RatingScale Often = new(2, "Often");
        public static RatingScale Sometimes = new(3, "Sometimes");
        public static RatingScale Rarely = new(4, "Rarely");
        public static RatingScale Never = new(5, "Never");

        public RatingScale(int id, string name) : base(id, name)
        {
        }
    }
}

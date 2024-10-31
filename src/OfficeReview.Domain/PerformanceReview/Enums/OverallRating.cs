namespace OfficePerformanceReview.Domain.PerformanceReview.Enums
{
    public class OverallRating : Enumeration
    {
        public static OverallRating RoughDiamond => new(1, "Rough Diamond");
        public static OverallRating RisingStar => new(2, "Rising Star");
        public static OverallRating FutureLeader => new(3, "Future Leader");
        public static OverallRating CorePerformer => new(4, "Core Performer");
        public static OverallRating HighPerformer => new(5, "High Performer");
        public static OverallRating SolidPerformer => new(6, "Solid Performer");
        public static OverallRating TechnicalExpert => new(7, "Technical Expert");

        public OverallRating(int id, string name) : base(id, name)
        {
        }
    }
}

namespace OfficePerformanceReview.Domain.PerformanceReview.Enums
{
    public class OverallRating : Enumeration
    {
        public static OverallRating RoughDiamond => new(1, "Rough Diamond", 10);
        public static OverallRating RisingStar => new(2, "Rising Star", 13);
        public static OverallRating FutureLeader => new(3, "Future Leader", 17);
        public static OverallRating CorePerformer => new(4, "Core Performer", 13);
        public static OverallRating HighPerformer => new(5, "High Performer", 17);
        public static OverallRating SolidPerformer => new(6, "Solid Performer", 13);
        public static OverallRating TechnicalExpert => new(7, "Technical Expert", 17);
        public static OverallRating InconsistentPerformer => new(8, "Inconsistent Performer", 10);
        public static OverallRating Risk => new(9, "Risk", 10);
        public OverallRating(int id, string name, int increasePercents) : base(id, name)
        {
            IncreasePercents = increasePercents;
        }
        public int IncreasePercents { get; private set; }
    }
}

namespace OfficePerformanceReview.Domain.ReviewCycle.Enums
{
    public class ReviewCycleStatus : Enumeration
    {
        public static readonly ReviewCycleStatus Draft = new(1, nameof(Draft));
        public static readonly ReviewCycleStatus Published = new(2, nameof(Published));
        public static readonly ReviewCycleStatus Frozen = new(3, nameof(Frozen));
        public ReviewCycleStatus(int id, string name) : base(id, name)
        {
        }
    }
}

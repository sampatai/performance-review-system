namespace OfficePerformanceReview.Domain.ReviewCycle.Enums
{
    public class ReviewCycleFrequency : Enumeration
    {
        public static readonly ReviewCycleFrequency Quarterly = new(1, nameof(Quarterly), TimeSpan.FromDays(90));
        public static readonly ReviewCycleFrequency BiAnnually = new(2, nameof(BiAnnually), TimeSpan.FromDays(182));
        public static readonly ReviewCycleFrequency Annually = new(3, nameof(Annually), TimeSpan.FromDays(365));

        public TimeSpan? Duration { get; }
        public ReviewCycleFrequency(int id, string name) : base(id, name)
        {
        }
        private ReviewCycleFrequency(int id, string name, TimeSpan? duration) : base(id, name)
        {
            Duration = duration;
        }

    }

}


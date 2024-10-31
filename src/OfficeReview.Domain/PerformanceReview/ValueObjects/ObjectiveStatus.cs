namespace OfficePerformanceReview.Domain.PerformanceReview.ValueObjects
{
    public class ObjectiveStatus : Enumeration
    {
        public static ObjectiveStatus Incomplete => new(1, "Incomplete");
        public static ObjectiveStatus Started => new(2, "Started");
        public static ObjectiveStatus Complete => new(3, "Complete");
        public ObjectiveStatus(int id, string name) : base(id, name)
        {
        }
    }
}

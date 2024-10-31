namespace OfficePerformanceReview.Domain.PerformanceReview.Enums
{
    public class Session : Enumeration
    {
        public static Session First = new(1, "First Session");
        public static Session Second = new(2, "Second Session");
        public Session(int id, string name) : base(id, name)
        {
        }
    }
}

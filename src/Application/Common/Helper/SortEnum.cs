using OfficeReview.Shared.SeedWork;

namespace OfficePerformanceReview.Application.Common.Helper
{
    public class SortEnum : Enumeration
    {
        public static SortEnum Asc = new(1, "asc");
        public static SortEnum Desc = new(2, "desc");
        public SortEnum(int id, string name) : base(id, name)
        {
        }
    }
}

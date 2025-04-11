
namespace OfficePerformanceReview.Application.Common.Model
{
    public abstract record PageList<T>
    {
        public IEnumerable<T> Data { get; set; }
        public int TotalRecords { get; set; }
    }
}

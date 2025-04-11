

namespace OfficePerformanceReview.Application.Common.Model
{
   public  record NameValueBase(string Name);
    public record NameValue : NameValueBase
    {
      public   long Id { get; set; }
        public NameValue(long id, string name) : base(name)
        {
            this.Id = id;
        }
    }
    public record NameValueInt : NameValueBase
    {
        public int Id { get; set; }

        public NameValueInt( int id, string name) : base(name)
        {
            this.Id = id;
        }
    }
}

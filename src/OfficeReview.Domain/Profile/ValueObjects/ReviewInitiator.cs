namespace OfficeReview.Domain.Profile.ValueObjects
{
    public class ReviewInitiator : ValueObject
    {
        public int StaffId { get; private set; }
        public string Name { get; private set; }


        protected ReviewInitiator() { }

        public ReviewInitiator(int staffId, string name)
        {
            StaffId = staffId;
            Name = Guard.Against.NullOrEmpty(name);

        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return StaffId;
            yield return Name;       
        }
    }
}

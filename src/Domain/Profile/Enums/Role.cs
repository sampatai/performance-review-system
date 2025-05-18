
namespace OfficeReview.Domain.Profile.Enums
{
    public class Role : Enumeration
    {
        public static Role Admin = new(1, nameof(Admin));
        public static Role Manager = new(2, nameof(Manager));
        public static Role Employee = new(3, nameof(Employee));
        public static Role HR = new(4, nameof(HR));

        public Role(int id, string name) : base(id, name)
        {
        }
        
    }
}


namespace OfficeReview.Domain.Profile.Enums
{
    public class Role : Enumeration
    {
        public static Role Admin = new(1, nameof(Admin));
        public static Role Reviewer = new(2, nameof(Reviewer));
        public static Role Appraisee = new(3, nameof(Appraisee));
      
        public Role(int id, string name) : base(id, name)
        {
        }
    }
}

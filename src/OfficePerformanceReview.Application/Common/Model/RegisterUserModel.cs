namespace OfficePerformanceReview.Application.Common.Model
{
    public record RegisterUserModel(string FirstName, string LastName, string Email, int Team, long Role);

    public record UserModel(Guid Id, string FirstName, string LastName, string Email, NameValue Team, NameValue Role);

    public record EditUserModel : RegisterUserModel
    {
        public EditUserModel(string firstName,
            string lastName,
            string email,
            int team,
            long role,
            Guid staffGuid)
            : base(firstName, lastName, email, team, role)
        {
            StaffGuid = staffGuid;
        }
        public Guid StaffGuid { get; set; }
    }

    public record UserList : PageList<UserModel> { }
}

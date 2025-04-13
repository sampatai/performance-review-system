namespace OfficePerformanceReview.Application.Common.Model
{
    public record RegisterUserModel(string FirstName, string LastName, string Email, int Team, int Role);

    public record UserModel(Guid StaffGuid, string FirstName, string LastName, string Email,NameValue Team,NameValue Role);

    public record UserList: PageList<UserModel> { }
}

namespace OfficePerformanceReview.Application.Common.Model
{
    public record RegisterUserModel(string FirstName, string LastName, string Email, int Team, int Role);

    public record UserModel(string FirstName, string LastName, string Email,NameValueInt Team,NameValue Role);

    public record UserList: PageList<UserModel> { }
}

namespace OfficePerformanceReview.Application.Common.Model
{
    public record RegisterUserModel(string FirstName, string LastName, string Email, int Team, long Role, long? ManagerId);

    public record UserModel(Guid Id, string FirstName, string LastName, string Email, NameValue Team, NameValue Role);
    public record Managers(long Id, string Name);

    public record EditUserModel(string FirstName,
            string LastName,
            string EmailAddress,
            int Team,
            long Role,
            long ?ManagerId,
            Guid StaffGuid) : RegisterUserModel(FirstName, LastName, EmailAddress, Team, Role, ManagerId);


    public record UserList : PageList<UserModel> { }
}

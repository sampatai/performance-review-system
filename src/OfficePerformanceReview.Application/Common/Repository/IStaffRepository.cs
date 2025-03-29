using OfficeReview.Shared.SeedWork;

namespace OfficePerformanceReview.Application.Common.Repository
{
    public interface IStaffRepository : IRepository<Staff>
    {
        Task AddToRoleAsync(Staff userToAdd, string playerRole);
        Task<IdentityResult> CreateAsync(Staff userToAdd, string password);
        Task<IdentityResult> AccessFailedAsync(Staff staff, CancellationToken cancellationToken);
        Task<IdentityResult> SetLockoutEndDateAsync(Staff staff, DateTime? date, CancellationToken cancellationToken);
        Task<IdentityResult> ResetAccessFailedCountAsync(Staff staff, CancellationToken cancellationToken);

    }
    public interface IReadonlyStaffRepository : IReadOnlyRepository<Staff>
    {
        Task<bool> CheckEmailExistsAsync(string email, CancellationToken cancellationToken);
        Task<IEnumerable<string>> GetRolesAsync(Staff staff);
        Task<Staff> FindByNameAsync(string userName);
        Task<SignInResult> CheckPasswordSignInAsync(Staff staff, string password, bool isLocked, CancellationToken cancellationToken);
        Task<Staff> FindByIdAsync(string staffId);

    }
}

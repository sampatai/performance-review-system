
using OfficeReview.Shared.SeedWork;

namespace OfficePerformanceReview.Application.Repository
{
    public interface IStaffRepository : IRepository<Staff>
    {
        Task AddToRoleAsync(Staff userToAdd, string playerRole);
        Task<IdentityResult> CreateAsync(Staff userToAdd, string password);
    }
    public interface IReadonlyStaffRepository : IReadOnlyRepository<Staff>
    {
        Task<bool> CheckEmailExistsAsync(string email, CancellationToken cancellationToken);
    }
}

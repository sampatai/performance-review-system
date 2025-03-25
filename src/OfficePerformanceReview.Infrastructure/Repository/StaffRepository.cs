using Microsoft.AspNetCore.Identity;
using System.Threading;

namespace OfficePerformanceReview.Infrastructure.Repository
{
    public class StaffRepository(PerformanceReviewDbContext performanceReviewDbContext,
    ILogger<StaffRepository> logger,
    UserManager<Staff> userManager) : IStaffRepository
    {
        public IUnitOfWork UnitOfWork => performanceReviewDbContext;

        public async Task AddToRoleAsync(Staff userToAdd, string role)
        {
            try
            {
                await userManager.AddToRoleAsync(userToAdd, role);

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "@{userToAdd} @{role}", userToAdd, role);

                throw;
            }
            ;
        }

        public async Task<IdentityResult> CreateAsync(Staff userToAdd, string password)
        {
            try
            {
                return await userManager.CreateAsync(userToAdd, password);

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "@{userToAdd} @{password}", userToAdd, password);

                throw;
            }
        }
    }
    public class ReadonlyStaffRepository(ILogger<ReadonlyStaffRepository> logger,
    UserManager<Staff> userManager
    ) : IReadonlyStaffRepository
    {
        public async Task<bool> CheckEmailExistsAsync(string email, CancellationToken cancellationToken)
        {
            try
            {
                var a = !await userManager
                    .Users
                    .AnyAsync(x => x.Email == email.ToLower(), cancellationToken);
                return await userManager
                    .Users
                    .AnyAsync(x => x.Email == email.ToLower(), cancellationToken);
            }
            catch (Exception ex)
            {

                logger.LogError(ex, "@{email}", email);
                throw; ;
            }

        }     
    }
}

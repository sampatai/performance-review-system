using Microsoft.AspNetCore.Identity;
using OfficePerformanceReview.Application.Common.Repository;
using OfficeReview.Domain.Profile.Enums;
using OfficeReview.Domain.Profile.Root;
using System.Threading;

namespace OfficePerformanceReview.Infrastructure.Repository
{
    public class StaffRepository(PerformanceReviewDbContext performanceReviewDbContext,
    ILogger<StaffRepository> logger,
    UserManager<Staff> userManager) : IStaffRepository
    {
        public IUnitOfWork UnitOfWork => performanceReviewDbContext;

        public async Task<IdentityResult> AccessFailedAsync(Staff staff, CancellationToken cancellationToken)
        {
            try
            {
                return await userManager.AccessFailedAsync(staff);

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "@{staff} ", staff);

                throw;
            }
        }

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

        public async Task<IdentityResult> ResetAccessFailedCountAsync(Staff staff, CancellationToken cancellationToken)
        {
            try
            {
                return await userManager.ResetAccessFailedCountAsync(staff);

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "@{staff}", staff);

                throw;
            }
        }

        public async Task<IdentityResult> SetLockoutEndDateAsync(Staff staff, DateTime? date, CancellationToken cancellationToken)
        {
            try
            {
                return await userManager.SetLockoutEndDateAsync(staff,date);

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "@{staff}", staff);

                throw;
            }
        }
    }
    public class ReadonlyStaffRepository(ILogger<ReadonlyStaffRepository> logger,
    UserManager<Staff> userManager,
    SignInManager<Staff> signInManager
    ) : IReadonlyStaffRepository
    {
        public async Task<bool> CheckEmailExistsAsync(string email, CancellationToken cancellationToken)
        {
            try
            {

                return !await userManager
                    .Users
                    .AnyAsync(x => x.Email == email.ToLower(), cancellationToken);
            }
            catch (Exception ex)
            {

                logger.LogError(ex, "@{email}", email);
                throw; ;
            }

        }

        public async Task<SignInResult> CheckPasswordSignInAsync(Staff staff, string password, bool isLocked, CancellationToken cancellationToken)
        {
            try
            {

                return await signInManager
                    .CheckPasswordSignInAsync(staff, password, isLocked); ;
            }
            catch (Exception ex)
            {

                logger.LogError(ex, "@{staff}", staff);
                throw;
            }
        }

        public async Task<Staff> FindByNameAsync(string userName)
        {
            try
            {
                return await userManager.FindByNameAsync(userName); ;
            }
            catch (Exception ex)
            {

                logger.LogError(ex, "@{userName}", userName);
                throw;
            }
        }

        public async Task<IEnumerable<string>> GetRolesAsync(Staff staff)
        {
            try
            {

                return await userManager.GetRolesAsync(staff);
            }
            catch (Exception ex)
            {

                logger.LogError(ex, "@{staff}", staff);
                throw;
            }
        }
    }
}

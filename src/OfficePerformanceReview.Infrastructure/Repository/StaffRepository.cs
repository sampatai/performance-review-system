using OfficePerformanceReview.Application.Common.Model;
using OfficePerformanceReview.Infrastructure.Extension;
using System.Linq.Expressions;


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
                return await userManager.SetLockoutEndDateAsync(staff, date);

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
    SignInManager<Staff> signInManager,
        PerformanceReviewDbContext performanceReviewDbContext
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
                throw;
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
                return await userManager.FindByNameAsync(userName);
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
        public async Task<Staff> FindByIdAsync(string staffId)
        {
            try
            {
                return await userManager.FindByIdAsync(staffId); ;
            }
            catch (Exception ex)
            {

                logger.LogError(ex, "@{staffId}", staffId);
                throw;
            }
        }

        public async Task<(IEnumerable<UserModel> users, int totalCount)> GetStaffAsync(FilterBase filter, CancellationToken cancellationToken)
        {
            try
            {

                var query = (from u in performanceReviewDbContext.Users.Include(x => x.Team)
                             join ur in performanceReviewDbContext.UserRoles on u.Id equals ur.UserId
                             join r in performanceReviewDbContext.Roles on ur.RoleId equals r.Id
                             select new { u, r });

                var likeSearchTerm = $"%{filter.SearchTerm}%";

                query = query.Where(x => EF.Functions.Like(x.u.FirstName, likeSearchTerm) ||
                                         EF.Functions.Like(x.u.LastName, likeSearchTerm) ||
                                         EF.Functions.Like(x.u.Email, likeSearchTerm) ||
                                         EF.Functions.Like(x.r.Name, likeSearchTerm));


                int totalRecords = await query.CountAsync(cancellationToken);
                var columnMap = GetStaffSortColumnMap();


                var users = query
                    .Select(a => new UserModel(a.u.StaffGuid, a.u.FirstName,
                    a.u.LastName, a.u.Email!,
                    new NameValue(a.u.Team.Id, a.u.Team.Name),
                    new NameValue(a.r.Id, a.r.Name!)))
                    .AsQueryable();

                var results = await users.ApplySorting(
                                     filter.SortColumn,
                                     filter.SortDirection,
                                     columnMap)
                  .Skip((filter.PageNumber - 1) * filter.PageSize)
                  .Take(filter.PageSize)
                  .ToListAsync(cancellationToken);
                return (results, totalRecords);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "GetStaffAsync {filter}", filter);
                throw;
            }
        }

        private Dictionary<string, Expression<Func<UserModel, object>>> GetStaffSortColumnMap()
        {
            return new Dictionary<string, Expression<Func<UserModel, object>>>(StringComparer.OrdinalIgnoreCase)
           {
            { "firstName", x => x.FirstName },
            { "lastName", x => x.LastName },
            { "email", x => x.Email },
            { "team", x => x.Team.Name },
            { "role", x => x.Role.Name },
          };
        }
    }
}
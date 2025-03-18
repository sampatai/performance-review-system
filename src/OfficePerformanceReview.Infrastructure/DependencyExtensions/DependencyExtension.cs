using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OfficeReview.Domain.Profile.Root;
using System.Reflection;


namespace OfficePerformanceReview.Infrastructure.DependencyExtensions
{
    public static class DependencyExtension
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddDbContext<PerformanceReviewDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                    });
            },
           ServiceLifetime.Scoped);
            services.AddDataProtection();

            services.AddIdentityCore<Staff>(options =>
            {
                // Password configuration
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = true;

                // For email confirmation
                options.SignIn.RequireConfirmedEmail = false;
            })
            .AddRoles<IdentityRole<long>>() // Use IdentityRole<long>
            .AddRoleManager<RoleManager<IdentityRole<long>>>() // Use RoleManager with long as the key type
            .AddEntityFrameworkStores<PerformanceReviewDbContext>() // Provide our context
            .AddSignInManager<SignInManager<Staff>>() // Use SignInManager
            .AddUserManager<UserManager<Staff>>() // Use UserManager to create users
            .AddDefaultTokenProviders(); // Enable token providers for email confirmation


            return services;
        }

    }
}

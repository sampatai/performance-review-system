using Amazon.Runtime;
using Amazon.SecretsManager;
using Amazon;
using Microsoft.Extensions.Options;
using OfficePerformanceReview.Application.Common.Helper;
using OfficePerformanceReview.Application.Common.Service;
using OfficePerformanceReview.Infrastructure.Secrets;
using OfficePerformanceReview.Application.Common.Options;
using static System.Collections.Specialized.BitVector32;
using System;
using Amazon.Extensions.NETCore.Setup;

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
                        sqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 15,
                            maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorNumbersToAdd: null
                        );
                    });

                // This sets the default tracking behavior to NoTracking
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            }, ServiceLifetime.Scoped);

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

            services.AddScoped<IStaffRepository, StaffRepository>();
            services.AddScoped<IReadonlyStaffRepository, ReadonlyStaffRepository>();
            services.AddScoped<IEvaluationFormTemplateRepository, EvaluationFormTemplateRepository>();
            services.AddScoped<IReadonlyEvaluationFormTemplateRepository, ReadonlyEvaluationFormTemplateRepository>();

            services._RegisterServices();
            return services;
        }
        private static void _RegisterServices(this IServiceCollection services)
        {



            // Best Practice: Use the AWS SDK's built-in extension methods for DI.
            // This registers the AmazonSecretsManagerClient with the correct lifecycle (Singleton by default).
            // Add configuration
            services.Configure<AWSOptions>();

            // Bind AWSOptions and register AWS SDK
            services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions("AWS"));
            services.AddAWSService<IAmazonSecretsManager>();

            // Register your custom service. The dependencies (IAmazonSecretsManager, IOptions<AwsOptions>, ILogger)
            // will be automatically injected by the service container.
            services.AddSingleton<IAwsSecretService, AwsSecretService>();

        }

    }
}

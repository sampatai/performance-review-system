using Amazon.Runtime;
using Amazon.SecretsManager;
using Amazon;
using Microsoft.Extensions.Options;
using OfficePerformanceReview.Application.Common.Service;
using OfficePerformanceReview.Infrastructure.Secrets;
using OfficePerformanceReview.Application.Common.Options;

using Amazon.Runtime.CredentialManagement;
using Microsoft.Extensions.DependencyInjection;



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


            services._RegisterServices(configuration);
            return services;
        }

        private static void _RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {



            // 2. Register AWS SDK default options.
            //    AddDefaultAWSOptions binds the general AWSOptions class from the "AWS" section
            //    of your configuration (e.g., region, profile).

            // 3. Register the AWS Secrets Manager client with credential handling via CredentialProfileStoreChain.
            services.AddSingleton<IAmazonSecretsManager>(sp =>
            {
                // Resolve the logger for use within this factory method for better diagnostics
                var logger = sp.GetRequiredService<ILogger<AmazonSecretsManagerClient>>();
                using var scope = services.BuildServiceProvider().CreateScope();

                var customAwsOptions = scope.ServiceProvider.GetRequiredService<IOptions<AWSConfigurationOptions>>().Value;
                AWSCredentials credentials = null;
                var region = RegionEndpoint.APSoutheast2; // Use your desired default region if not in config

                // Use CredentialProfileStoreChain if a profile name is provided in customAwsOptions
                if (!string.IsNullOrEmpty(customAwsOptions.CredentialProfile))
                {
                    var chain = new CredentialProfileStoreChain();
                    if (chain.TryGetAWSCredentials(customAwsOptions.CredentialProfile, out credentials))
                    {
                        logger.LogInformation("Using AWS credentials from profile '{ProfileName}'.", customAwsOptions.CredentialProfile);
                    }
                    else
                    {
                        logger.LogWarning("Failed to retrieve credentials for profile '{ProfileName}'. Falling back to default provider chain.", customAwsOptions.CredentialProfile);
                    }
                }

                // If credentials were found via CredentialProfileStoreChain, use them.
                // Otherwise, let the client use the default credential provider chain (environment variables, default profile, etc.).
                return credentials != null
                    ? new AmazonSecretsManagerClient(credentials, region)
                    : new AmazonSecretsManagerClient(region);
            });

            // 4. Register your custom AwsSecretService.
            //    The DI container will now correctly resolve IOptions<AwsConfigurationOptions>,
            //    ILogger<AwsSecretService>, and IAmazonSecretsManager and inject them into
            //    the AwsSecretService constructor.
            services.AddSingleton<IAwsSecretService, AwsSecretService>();
        }

    }
}

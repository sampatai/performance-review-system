using OfficePerformanceReview.Application.Behaviors;
using OfficePerformanceReview.Application.Common.Helper;
using OfficePerformanceReview.Application.Common.Service;

namespace OfficePerformanceReview.Application.DependencyExtensions
{
    public static class DependencyExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {

            var applicationAssembly = Assembly.GetExecutingAssembly();


            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(applicationAssembly);
                config.AddOpenBehavior(typeof(ValidatorBehavior<,>));
                config.AddOpenBehavior(typeof(LoggingBehavior<,>));

            });

            // Register FluentValidation validators
            services.AddValidatorsFromAssembly(applicationAssembly, includeInternalTypes: true);
            _RegisterServices(services);
            return services;
        }
        private static void _RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IJWTService, JWTService>();
            services.AddScoped<ICurrentUserHelper, HttpContextCurrentUserHelper>();
        }
       

    }
}


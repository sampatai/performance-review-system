using OfficePerformanceReview.Application.Common.Helper;
using System.Linq.Expressions;
using System.Reflection;


namespace OfficePerformanceReview.Infrastructure.Extension
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> queryable,
        bool condition,
        Expression<Func<T, bool>> predicate)
        {
            if (condition) return queryable.Where(predicate);
            return queryable;
        }
      
    
        public static IQueryable<T> ApplySorting<T>(
            this IQueryable<T> source,
            string sortProperty,
            string sortDirection = "asc")
        {
            if (string.IsNullOrWhiteSpace(sortProperty))
                return source;

            var entityType = typeof(T);
            var parameter = Expression.Parameter(entityType, "x");

            // Handle nested properties (e.g., "Team.Name")
            Expression propertyAccess = parameter;
            foreach (var propertyName in sortProperty.Split('.'))
            {
                var property = entityType.GetProperty(propertyName,
                    BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (property == null)
                    return source; // or throw exception if preferred

                propertyAccess = Expression.Property(propertyAccess, property);
                entityType = property.PropertyType;
            }

            var lambda = Expression.Lambda(propertyAccess, parameter);

            string methodName = sortDirection.Equals("desc", StringComparison.OrdinalIgnoreCase)
                ? "OrderByDescending"
                : "OrderBy";

            var resultExpression = Expression.Call(
                typeof(Queryable),
                methodName,
                new Type[] { typeof(T), propertyAccess.Type },
                source.Expression,
                Expression.Quote(lambda));

            return source.Provider.CreateQuery<T>(resultExpression);
        }
    }
}

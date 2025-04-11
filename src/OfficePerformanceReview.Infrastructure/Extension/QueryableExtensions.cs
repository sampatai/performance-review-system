using OfficePerformanceReview.Application.Common.Helper;
using System.Linq.Expressions;


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
        string? sortColumn,
        string? sortDirection,
        Dictionary<string, Expression<Func<T, object>>> columnMap,
        Expression<Func<T, object>>? defaultSort = null)
        {
            if (string.IsNullOrWhiteSpace(sortColumn) || !columnMap.TryGetValue(sortColumn.Trim(), out var sortExpression))
            {
                return defaultSort != null ? source.OrderByDescending(defaultSort) : source;
            }

            bool descending = string.Equals(sortDirection, SortEnum.Desc.Name, StringComparison.OrdinalIgnoreCase);

            return descending
                ? source.OrderByDescending(sortExpression)
                : source.OrderBy(sortExpression);
        }
    }
}

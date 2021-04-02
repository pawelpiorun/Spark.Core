using Spark.Core.Shared.DTOs;
using System.Linq;

namespace Spark.Core.Server.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable,
            PaginationDTO pagination)
        {
            if (pagination is null) return queryable;

            return queryable.Skip((pagination.Page - 1) * pagination.ItemsPerPage)
                .Take(pagination.ItemsPerPage);
        }
    }
}

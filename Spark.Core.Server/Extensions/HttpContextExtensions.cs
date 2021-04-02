using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Calculo.Server.Extensions
{
    public static class HttpContextExtensions
    {
        public async static Task InsertPaginationParametersInResponse<T>(this HttpContext httpContext,
            IQueryable<T> queryable, int itemsPerPage)
        {
            if (httpContext is null)
                throw new ArgumentNullException(nameof(httpContext));

            double count = await queryable.CountAsync();
            var totalAmountOfPages = (int)(Math.Ceiling(count / itemsPerPage));
            httpContext.Response.Headers.Add("totalPages", totalAmountOfPages.ToString());
        }
    }
}

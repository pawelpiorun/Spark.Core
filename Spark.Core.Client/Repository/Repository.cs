using Spark.Core.Shared.DTOs;
using Spark.Core.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spark.Core.Client.Repository
{
    public abstract class Repository<T, TID> : IRepository<T, TID>
    {
        protected readonly IHttpService httpService;
        public Repository(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        protected abstract string Url { get; }

        public async Task<List<T>> GetEntriesAsync()
        {
            var response = await httpService.Get<List<T>>(Url);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
            return response.Response.ToList();
        }

        public async Task<TID> CreateAsync(T entry)
        {
            var response = await httpService.Post<T,TID>(Url, entry);
            if (!response.Success)
                throw new ApplicationException(await response.GetBody());

            return response.Response;
        }

        public virtual async Task<List<T>> GetEntriesByAsync(string propertyName, string value)
        {
            var property = typeof(T).GetProperty(propertyName);
            if (property is null || property.PropertyType != typeof(string))
                throw new ArgumentException(nameof(propertyName));

            var response = await httpService.Get<List<T>>(Url + $"/search/{propertyName}/{value}");
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
            return response.Response.ToList();
        }

        public virtual async Task<T> GetEntryAsync(TID id)
        {
            var response = await httpService.Get<T>(Url + $"/{id}");
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }

        public async Task UpdateEntryAsync(T entry)
        {
            var response = await httpService.Put<T>(Url, entry);
            if (!response.Success)
                throw new ApplicationException(await response.GetBody());
        }

        public async Task RemoveEntryAsync(TID id)
        {
            var response = await httpService.Delete(Url + $"/{id}");
            if (!response.Success)
                throw new ApplicationException(await response.GetBody());
        }


        public async Task<PaginatedResponse<List<T>>> GetPaginatedEntriesAsync(PaginationDTO pagination)
        {
            string queryUrl = Url;
            string query = $"page={pagination.Page}&itemsPerPage={pagination.ItemsPerPage}";

            queryUrl += Url.Contains("?") ? "&" : "?";
            queryUrl += query;

            var responseWrapper = await httpService.Get<List<T>>(queryUrl);
            if (!responseWrapper.Success)
            {
                throw new ApplicationException(await responseWrapper.GetBody());
            }

            var totalPagesHeader = responseWrapper.ResponseMessage.Headers.GetValues("totalPages").FirstOrDefault();
            var totalAmountOfPages = int.Parse(totalPagesHeader);

            var paginatedResponse = new PaginatedResponse<List<T>>()
            {
                Response = responseWrapper.Response.ToList(),
                TotalPages = totalAmountOfPages
            };
            return paginatedResponse;
        }

        public async Task<PaginatedResponse<List<T>>> GetFilteredPaginatedEntriesAsync(
            FilterOperationDTO filter)
        {
            var responseHttp = await httpService.Post<FilterOperationDTO, List<T>>(
                $"{Url}/filter", filter);

            var totalPagesHeader = responseHttp.ResponseMessage.Headers.GetValues("totalPages").FirstOrDefault();
            var totalAmountOfPages = int.Parse(totalPagesHeader);

            var paginatedResponse = new PaginatedResponse<List<T>>()
            {
                Response = responseHttp.Response,
                TotalPages = totalAmountOfPages
            };

            return paginatedResponse;
        }
    }
}

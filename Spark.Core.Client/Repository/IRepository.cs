using Spark.Core.Shared.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spark.Core.Client.Repository
{
    public interface IRepository
    {

    }
    
    public interface IRepository<T> : IRepository
    {
        Task<List<T>> GetEntriesAsync();
        Task<List<T>> GetEntriesByAsync(string propertyName, string value);
        Task<PaginatedResponse<List<T>>> GetPaginatedEntriesAsync(PaginationDTO pagination);
        Task<PaginatedResponse<List<T>>> GetFilteredPaginatedEntriesAsync<TFilter>(EntityFilterDTO<TFilter> filter);
        Task UpdateEntryAsync(T entry);
    }

    public interface IRepository<T, TID> : IRepository<T>
    {
        Task<TID> CreateAsync(T entry);
        Task<T> GetEntryAsync(TID id);
        Task RemoveEntryAsync(TID id);
    }
}

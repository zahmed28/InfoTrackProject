using Microsoft.Extensions.Hosting;
using SearchReadService.Domain;

namespace SearchReadService.Infrastructure.Repositories
{
    public interface ISearchHistoryRepository
    {
        Task<PagedResult<SearchResults>> GetAllSearchHistoryAsync(PagingParameters pagingParameters);
    }
}

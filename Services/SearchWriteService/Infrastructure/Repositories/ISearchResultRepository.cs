using Microsoft.Extensions.Hosting;
using SearchWriteService.Domain;

namespace SearchWriteService.Infrastructure.Repositories
{
    public interface ISearchResultRepository
    {
        Task<SearchResults> CreateSearchResultsAsync(SearchResults searchResults);
    }
}

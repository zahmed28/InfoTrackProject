using SearchWriteService.Domain;
using SearchWriteService.Infrastructure.Repositories;

namespace SearchWriteService.Infrastructure.Persistence
{
    public class SearchResultRepository : ISearchResultRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<SearchResultRepository> _logger;
        public SearchResultRepository(AppDbContext context, ILogger<SearchResultRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context), "AppDbContext cannot be null.");
            _logger = logger ?? throw new ArgumentNullException(nameof(logger), "Logger cannot be null.");
        }

        public async Task<SearchResults> CreateSearchResultsAsync(SearchResults searchResults)
        {
            
            if (searchResults == null)
            {
                throw new ArgumentNullException(nameof(searchResults), "Provided search results cannot be null.");
            }

            try
            {
                _context.SearchResults.Add(searchResults);
                await _context.SaveChangesAsync();
                return searchResults;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating search results: {ex.Message}", ex);
                throw;
            }
        }
    }
}

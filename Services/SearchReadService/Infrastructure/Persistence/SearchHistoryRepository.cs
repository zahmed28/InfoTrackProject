using Microsoft.EntityFrameworkCore;
using SearchReadService.Domain;
using SearchReadService.Infrastructure.Repositories;

namespace SearchReadService.Infrastructure.Persistence
{
    public class SearchHistoryRepository: ISearchHistoryRepository
    {
        private readonly AppDbContext _context;

        private readonly ILogger<SearchHistoryRepository> _logger;

        public SearchHistoryRepository(AppDbContext context, ILogger<SearchHistoryRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context), "AppDbContext cannot be null.");
            _logger = logger ?? throw new ArgumentNullException(nameof(logger), "Logger cannot be null.");
        }

        public async Task<PagedResult<SearchResults>> GetAllSearchHistoryAsync(PagingParameters pagingParameters)
        {
            try
            {
                if (pagingParameters == null)
                {
                    throw new ArgumentNullException(nameof(pagingParameters), "Paging parameters cannot be null.");
                }

                var query = _context.SearchResults.AsNoTracking(); // Use AsNoTracking for read-only operations for better performance

                var totalItemCount = await query.CountAsync();
                
                var pagedItems = await query
                                    .OrderByDescending(item => item.DateCreated)
                                    .Skip((pagingParameters.PageNumber - 1) * pagingParameters.PageSize)
                                    .Take(pagingParameters.PageSize)
                                    .ToListAsync();

                return new PagedResult<SearchResults>
                {
                    Records = pagedItems,
                    TotalRecords = totalItemCount,
                    CurrentPageSize = pagingParameters.PageSize,
                    CurrentPage = pagingParameters.PageNumber,
                    TotalPages = (int)Math.Ceiling(totalItemCount / (double)pagingParameters.PageSize)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving search history.");
                throw; 
            }
        }
    }
}

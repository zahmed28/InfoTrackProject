using MediatR;
using SearchReadService.Application.Queries;
using SearchReadService.Domain;
using SearchReadService.Infrastructure.Repositories;

namespace SearchReadService.Application.Handlers
{
    public class GetSearchHistoryHandler : IRequestHandler<GetSearchHistoryQuery, PagedResult<SearchResults>>
    {
        private readonly ISearchHistoryRepository _searchHistoryRepository;
        private readonly ILogger<GetSearchHistoryHandler> _logger;
        public GetSearchHistoryHandler(ISearchHistoryRepository searchHistoryRepository, ILogger<GetSearchHistoryHandler> logger)
        {            
            _searchHistoryRepository = searchHistoryRepository ?? throw new ArgumentNullException(nameof(searchHistoryRepository), "SearchHistoryRepository cannot be null.");
            _logger = logger ?? throw new ArgumentNullException(nameof(logger), "Logger cannot be null.");
        }
        public async Task<PagedResult<SearchResults>> Handle(GetSearchHistoryQuery request, CancellationToken cancellationToken)
        {
            PagingParameters pagingParameters = new PagingParameters();
            pagingParameters.PageNumber = request.PageNumber;
            pagingParameters.PageSize = request.PageSize;

            try
            {
                var result = await _searchHistoryRepository.GetAllSearchHistoryAsync(pagingParameters);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving search history");
                throw;
            }
            
        }
    }
}

using MediatR;
using SearchWriteService.Application.Commands;
using SearchWriteService.Domain;
using SearchWriteService.Infrastructure.Repositories;

namespace SearchWriteService.Application.Handlers
{
    public class CreateSearchResultHandler : IRequestHandler<CreateSearchResultCommand, SearchResults>
    {
        private readonly ISearchResultRepository _searchResultRepository;

        private readonly ILogger<CreateSearchResultHandler> _logger;
        public CreateSearchResultHandler(ISearchResultRepository searchResultRepository, ILogger<CreateSearchResultHandler> logger)
        {            
            _searchResultRepository = searchResultRepository ?? throw new ArgumentNullException(nameof(searchResultRepository), "SearchResultRepository cannot be null.");
            _logger = logger ?? throw new ArgumentNullException(nameof(logger), "Logger cannot be null.");
        }

        public async Task<SearchResults> Handle(CreateSearchResultCommand request, CancellationToken cancellationToken)
        {
            var searchResults = new SearchResults() {                
                Query = request.Query,
                ResultURL = request.ResultURL,
                RankingIndices = request.RankingIndices,
                DateCreated = DateTime.UtcNow,

            };

            try
            {
                _logger.LogInformation("Creating search results for query {Query}", request.Query);
                var result = await _searchResultRepository.CreateSearchResultsAsync(searchResults);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating search results for query {Query}", request.Query);
                throw; 
            }           
        }

    }
}

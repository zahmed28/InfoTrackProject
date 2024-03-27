using GoogleWebScrapperService.Application.Queries;
using GoogleWebScrapperService.Domain;
using GoogleWebScrapperService.Infrastructure.Persistance;
using GoogleWebScrapperService.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.Hosting;

namespace GoogleWebScrapperService.Application.Handlers
{
    public class GetSearchHandler : IRequestHandler<GetSearchQuery, Ranking>
    {
        private readonly ISearchRepository _searchRepository;
        public GetSearchHandler(ISearchRepository searchRepository)
        {
            _searchRepository = searchRepository;
        }

        public async Task<Ranking> Handle(GetSearchQuery request, CancellationToken cancellationToken)
        {
            Ranking result = new();
            result.RankingPosition = await _searchRepository.GetSearchResultAsync(request.Keyword, request.URL);                      
            return result;
        }
    }
}

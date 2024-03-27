using MediatR;
using SearchWriteService.Domain;

namespace SearchWriteService.Application.Commands
{
    public class CreateSearchResultCommand : IRequest<SearchResults>
    {
        public string Query { get; set; }
        public string ResultURL { get; set; }
        public string RankingIndices { get; set; }
    }
}

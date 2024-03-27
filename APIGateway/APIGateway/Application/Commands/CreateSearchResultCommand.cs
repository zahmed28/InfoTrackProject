using APIGateway.Domain.Entities;
using MediatR;

namespace APIGateway.Application.Commands
{
    public class CreateSearchResultCommand : IRequest<SearchResults>
    {
        public string Query { get; set; }
        public string ResultURL { get; set; }
        public string RankingIndices { get; set; }
    }
}

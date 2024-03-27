using APIGateway.Domain.Entities;
using MediatR;

namespace APIGateway.Application.Queries
{
    public class GetSearchQuery : IRequest<Ranking>
    {
        public string Keyword { get; set; }
        public string URL { get; set; }
    }
}

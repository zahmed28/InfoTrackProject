using APIGateway.Domain.Entities;
using MediatR;

namespace APIGateway.Application.Queries
{
    public class GetSearchHistoryQuery : IRequest<PagedResult<SearchResults>>
    {
        public int PageNumber { get; set; } = 1; // Default value is 1
        public int PageSize { get; set; } = 50; // Default value is 50
    }
}

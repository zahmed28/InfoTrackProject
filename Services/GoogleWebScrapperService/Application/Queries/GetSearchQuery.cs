using GoogleWebScrapperService.Domain;
using MediatR;

namespace GoogleWebScrapperService.Application.Queries
{
    public class GetSearchQuery:IRequest<Ranking>
    {
        public string Keyword { get; set; }
        public string URL {  get; set; }
    }
}

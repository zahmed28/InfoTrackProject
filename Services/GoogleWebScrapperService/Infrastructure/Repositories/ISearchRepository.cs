namespace GoogleWebScrapperService.Infrastructure.Repositories
{
    public interface ISearchRepository
    {
        Task<List<int>> GetSearchResultAsync(string keyword, string url);
    }
}

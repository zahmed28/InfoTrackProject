using GoogleWebScrapperService.Infrastructure.Repositories;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;

namespace GoogleWebScrapperService.Infrastructure.Persistance
{
    public class SearchRepository : ISearchRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<SearchRepository> _logger;

        public SearchRepository(IHttpClientFactory httpClientFactory, ILogger<SearchRepository> logger)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory), "HttpClientFactory cannot be null.");
            _logger = logger ?? throw new ArgumentNullException(nameof(logger), "Logger cannot be null.");
        }

        public async Task<List<int>> GetSearchResultAsync(string keyword, string url)
        {
            var indices = new List<int>();

            // URL of the website to scrape
            string googleScrapeUrl = $"https://www.google.com/search?num=100&q={HttpUtility.UrlEncode(keyword)}";

            try
            {
                var httpClient = _httpClientFactory.CreateClient();
                const string googleWebPageRegex = @"(?<=<div class=""egMi0 kCrYT""><a href=""/url\?q=)[^""]*";
                var response = await httpClient.GetStringAsync(googleScrapeUrl);
                var matches = Regex.Matches(response, googleWebPageRegex);

                int rank = 1;
                foreach (Match match in matches)
                {
                    if (match.Groups[0].Value.Contains(url))
                    {
                        indices.Add(rank);
                    }
                    rank++;
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"HTTP request for keyword '{keyword}' failed: {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred: {ex.Message}");
            }

            return indices;
        }
    }
}

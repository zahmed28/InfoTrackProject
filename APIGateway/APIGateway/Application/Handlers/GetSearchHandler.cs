using APIGateway.Application.Queries;
using APIGateway.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using APIGateway.Application.Commands;
using Microsoft.AspNetCore.Mvc;

namespace APIGateway.Application.Handlers
{
    public class GetSearchHandler : IRequestHandler<GetSearchQuery, Ranking>
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<GetSearchHandler> _logger;
        public GetSearchHandler(IConfiguration configuration, IHttpClientFactory clientFactory, ILogger<GetSearchHandler> logger)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _clientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
            _logger = logger ?? throw new ArgumentNullException(nameof(_logger));
        }

      
        public async Task<Ranking> Handle(GetSearchQuery request, CancellationToken cancellationToken)
        {
            var apiUrl = _configuration.GetValue<string>("GoogleWebScrapperServiceUrl") ?? throw new InvalidOperationException("API URL is not configured.");
            var requestUrl = $"{apiUrl}/Search";

            try
            {
                var ranking = await PostAsync<Ranking>(requestUrl, request, cancellationToken);
                var createSearchResultCommand = new CreateSearchResultCommand
                {
                    ResultURL = request.URL,
                    Query = request.Keyword,
                    RankingIndices = ranking.RankingPosition.Count != 0 ? string.Join(",", ranking.RankingPosition) : "0"
                };

                await CreateSearchResult(createSearchResultCommand, cancellationToken);
                return ranking;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while calling the Google Web Scraper.");
                throw; // Or handle it as per your error handling strategy
            }
        }      

        public async Task<SearchResults> CreateSearchResult(CreateSearchResultCommand command, CancellationToken cancellationToken)
        {
            var apiUrl = _configuration.GetValue<string>("SearchResultsWriteServiceUrl") ?? throw new InvalidOperationException("API URL for search results write service is not configured.");
            var requestUrl = $"{apiUrl}/api/SearchResult";

            return await PostAsync<SearchResults>(requestUrl, command, cancellationToken);
        }

        private async Task<T> PostAsync<T>(string requestUrl, object contentValue, CancellationToken cancellationToken) where T : new()
        {
            var client = _clientFactory.CreateClient();
            var json = JsonSerializer.Serialize(contentValue);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(requestUrl, content, cancellationToken);
            response.EnsureSuccessStatusCode(); 

            await using var responseStream = await response.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<T>(responseStream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }, cancellationToken);
            return result ?? new T();
        }

    }
}

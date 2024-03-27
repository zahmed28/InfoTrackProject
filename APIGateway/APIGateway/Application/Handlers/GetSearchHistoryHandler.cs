using APIGateway.Application.Commands;
using APIGateway.Application.Queries;
using APIGateway.Domain.Entities;
using MediatR;
using System.Text.Json;
using System.Text;

namespace APIGateway.Application.Handlers
{
    public class GetSearchHistoryHandler : IRequestHandler<GetSearchHistoryQuery, PagedResult<SearchResults>>
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<GetSearchHistoryHandler> _logger;


        public GetSearchHistoryHandler(IConfiguration configuration, IHttpClientFactory clientFactory, ILogger<GetSearchHistoryHandler> logger)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _clientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<PagedResult<SearchResults>> Handle(GetSearchHistoryQuery request, CancellationToken cancellationToken)
        {
            var apiUrl = _configuration.GetValue<string>("SearchResultsReadServiceUrl") ?? throw new InvalidOperationException("API URL is not configured.");
            var requestUrl = $"{apiUrl}/api/SearchHistory";

            var client = _clientFactory.CreateClient();
            HttpResponseMessage? response = null;
           
            try
            {
                var json = JsonSerializer.Serialize(request);
                using var content = new StringContent(json, Encoding.UTF8, "application/json");

                response = await client.PostAsync(requestUrl, content, cancellationToken);
                response.EnsureSuccessStatusCode();

                await using var responseStream = await response.Content.ReadAsStreamAsync(cancellationToken);
                var result = await JsonSerializer.DeserializeAsync<PagedResult<SearchResults>>(responseStream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }, cancellationToken); // Use cancellation token

                return result == null ? throw new JsonException("Failed to deserialize the response.") : result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while Google Web Scrapper: {ex.Message}");
                // Handle any errors that occurred during the request.             
                throw;
            }
            finally
            {
                response?.Dispose();
            }

        }
    }
}

using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SearchReadService.Application.Queries;

namespace SearchReadService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchHistoryController : ControllerBase
    {
        private readonly ILogger<SearchHistoryController> _logger;
        private readonly IMediator _mediator;

        public SearchHistoryController(ILogger<SearchHistoryController> logger, IMediator mediator)
        {            
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator), "mediator cannot be null.");
            _logger = logger ?? throw new ArgumentNullException(nameof(logger), "Logger cannot be null.");
        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllSearchHistory([FromQuery] GetSearchHistoryQuery query)
        {          
            try
            {
                _logger.LogInformation("Called SearchResultReadService GetAllSearchHistory method");
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the search result.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }
    }
}

using GoogleWebScrapperService.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GoogleWebScrapperService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly ILogger<SearchController> _logger;
        private readonly IMediator _mediator;
        public SearchController(ILogger<SearchController> logger, IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator), "mediator cannot be null.");
            _logger = logger ?? throw new ArgumentNullException(nameof(logger), "Logger cannot be null.");
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromBody] GetSearchQuery getSearchQuery)
        {           
            try
            {
                _logger.LogInformation("Called GoogleWebScrapperService Get method");
                var result = await _mediator.Send(getSearchQuery);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while calling GoogleWebScrapperService.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }
    }
}

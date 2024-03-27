using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SearchWriteService.Application.Commands;

namespace SearchWriteService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchResultController : ControllerBase
    {

        private readonly ILogger<SearchResultController> _logger;
        private readonly IMediator _mediator;

        public SearchResultController(ILogger<SearchResultController> logger, IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator), "mediator cannot be null.");
            _logger = logger ?? throw new ArgumentNullException(nameof(logger), "Logger cannot be null.");
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)] 
        [ProducesResponseType(StatusCodes.Status400BadRequest)] 
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] 
        public async Task<IActionResult> Create([FromBody] CreateSearchResultCommand createSearchResultCommand)
        {            
            try
            {
                _logger.LogInformation("Received request to create search result.");
                var result = await _mediator.Send(createSearchResultCommand);
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

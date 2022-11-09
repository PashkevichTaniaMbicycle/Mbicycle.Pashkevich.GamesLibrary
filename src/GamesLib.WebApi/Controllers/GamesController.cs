using MediatR;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using GamesLib.BusinessLogic.Handlers.Commands;
using GamesLib.BusinessLogic.Handlers.Queries;

namespace GamesLib.WebApi.Controllers
{
    [ApiController]
    [Route("api/games")]
    public class GamesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GamesController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("all")]
        public async Task<IActionResult> Get()
        {
            var query = new GetAllGamesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddGameCommand command)
        {
            var result = await _mediator.Send(command);
            
            if (result.Error)
            {
                return BadRequest(result);
            }

            return Created(HttpContext.Request.GetDisplayUrl(), result);
        }
    }
}
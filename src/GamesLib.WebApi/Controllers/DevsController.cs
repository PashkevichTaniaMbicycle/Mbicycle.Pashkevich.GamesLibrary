using MediatR;
using Microsoft.AspNetCore.Mvc;
using GamesLib.BusinessLogic.Handlers.Queries;

namespace GamesLib.WebApi.Controllers
{
    [ApiController]
    [Route("api/devs")]
    public class DevController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DevController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("all")]
        public async Task<IActionResult> Get()
        {
            var query = new GetAllDevsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

    }
}
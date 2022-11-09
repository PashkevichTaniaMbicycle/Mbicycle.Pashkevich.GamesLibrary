using MediatR;
using Microsoft.AspNetCore.Mvc;
using GamesLib.BusinessLogic.Handlers.Queries;

namespace GamesLib.WebApi.Controllers
{
    [ApiController]
    [Route("api/publishers")]
    public class PublisherController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PublisherController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("all")]
        public async Task<IActionResult> Get()
        {
            var query = new GetAllPublishersQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

    }
}
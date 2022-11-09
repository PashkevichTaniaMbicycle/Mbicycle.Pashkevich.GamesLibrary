using MediatR;
using Microsoft.AspNetCore.Mvc;
using GamesLib.BusinessLogic.Handlers.Queries.Publisher;
using GamesLib.BusinessLogic.Handlers.Commands.Publisher;
using Microsoft.AspNetCore.Http.Extensions;

namespace GamesLib.WebApi.Controllers;

[ApiController]
[Route("api/devs")]
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

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AddPublisherCommand command)
    {
        var result = await _mediator.Send(command);
        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Created(HttpContext.Request.GetDisplayUrl(), result);
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] UpdatePublisherCommand command)
    {
        var result = await _mediator.Send(command);
        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeletePublisherCommand { PublisherId = id };
        var result = await _mediator.Send(command);
        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
}
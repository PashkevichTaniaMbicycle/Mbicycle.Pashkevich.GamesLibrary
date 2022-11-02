using Microsoft.AspNetCore.Mvc;
using GamesLib.BusinessLogic;
using GamesLib.BusinessLogic.Dtos;

namespace GamesLib.WebApi.Controllers
{
    [ApiController]
    [Route("api/games")]
    public class GamesController : ControllerBase
    {
        private readonly IGamesService _gamesService;

        public GamesController(IGamesService gamesService)
        {
            _gamesService = gamesService ?? throw new ArgumentNullException(nameof(gamesService));
        }

        [HttpGet("all")]
        public IEnumerable<GameDto> Get()
        {
            return _gamesService.GetAllGames();
        }
    }
}
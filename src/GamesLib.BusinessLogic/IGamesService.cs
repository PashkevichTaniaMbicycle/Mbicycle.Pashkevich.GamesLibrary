using GamesLib.BusinessLogic.Dtos;

namespace GamesLib.BusinessLogic;

public interface IGamesService
{
    IEnumerable<GameDto> GetAllGames();
}
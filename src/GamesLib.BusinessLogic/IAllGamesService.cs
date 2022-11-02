using GamesLib.BusinessLogic.Dtos;

namespace GamesLib.BusinessLogic;

public interface IAllGamesService
{
    IEnumerable<GameDto> GetAllGames();
}
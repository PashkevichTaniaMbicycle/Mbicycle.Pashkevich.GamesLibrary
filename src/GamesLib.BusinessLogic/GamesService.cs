using GamesLib.BusinessLogic.Dtos;
using GamesLib.DataAccess.Repositories;

namespace GamesLib.BusinessLogic;

public class GamesService : IGamesService
{
    private readonly IGameRepository _gameRepository;

    public GamesService(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
    }

    public IEnumerable<GameDto> GetAllGames()
    {
        var result = _gameRepository.Get().Select(game =>
            new GameDto
            {
                Id = game.Id,
                Title = game.Title,
                Description = game.Description,
                Rating = game.Rating,
                ReleaseDate = game.ReleaseDate,
                DevName = game.Dev.Title,
                PublisherName = game.Publisher.Title,
            });

        return result;
    }
}
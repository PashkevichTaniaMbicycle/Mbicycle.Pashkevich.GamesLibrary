using MediatR;
using GamesLib.BusinessLogic.Dtos;
using GamesLib.BusinessLogic.Wrappers.Result;
using GamesLib.DataAccess.Repositories;

namespace GamesLib.BusinessLogic.Handlers.Queries.Game
{
    public class GetAllGamesQuery : IRequestResult<IEnumerable<GameDto>>
    {
    }

    public class GetAllGamesQueryHandler : IRequestHandlerResult<GetAllGamesQuery, IEnumerable<GameDto>>
    {
        private readonly IGameRepository _gameRepository;

        public GetAllGamesQueryHandler(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
        }

        async Task<Result<IEnumerable<GameDto>>> IRequestHandler<GetAllGamesQuery, Result<IEnumerable<GameDto>>>.Handle(GetAllGamesQuery request, CancellationToken cancellationToken)
        {
            var data = (await _gameRepository.GetAsync())
                .Select(game => new GameDto
                {
                    Id = game.Id,
                    Title = game.Title,
                    Description = game.Description,
                    Rating = game.Rating,
                    ReleaseDate = game.ReleaseDate,
                    DevTitle = game.Dev.Title,
                    PublisherTitle = game.Publisher.Title,
                });

            return Result.Success(data);
        }
    }
}
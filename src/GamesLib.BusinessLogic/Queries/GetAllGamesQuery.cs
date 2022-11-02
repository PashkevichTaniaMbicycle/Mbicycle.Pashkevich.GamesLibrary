using MediatR;
using GamesLib.BusinessLogic.Dtos;
using GamesLib.BusinessLogic.Wrappers;
using GamesLib.DataAccess.Repositories;

namespace GamesLib.BusinessLogic.Oueries
{
    public class GetAllGamesQuery : IRequest<Result<IEnumerable<GameDto>>>
    {
    }

    public class GetAllGamesQueryHandler : IRequestHandler<GetAllGamesQuery, Result<IEnumerable<GameDto>>>
    {
        private readonly IGameRepository _gameRepository;

        public GetAllGamesQueryHandler(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
        }

        Task<Result<IEnumerable<GameDto>>> IRequestHandler<GetAllGamesQuery, Result<IEnumerable<GameDto>>>.Handle(GetAllGamesQuery request, CancellationToken cancellationToken)
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

            return Task.FromResult(Result.Success(result));
        }
    }
}
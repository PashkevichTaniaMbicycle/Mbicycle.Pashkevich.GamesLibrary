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
        private readonly IGameRepository _saleRepository;

        public GetAllGamesQueryHandler(IGameRepository saleRepository)
        {
            _saleRepository = saleRepository ?? throw new ArgumentNullException(nameof(saleRepository));
        }

        async Task<Result<IEnumerable<GameDto>>> IRequestHandler<GetAllGamesQuery, Result<IEnumerable<GameDto>>>.Handle(GetAllGamesQuery request, CancellationToken cancellationToken)
        {
            var data = (await _saleRepository.GetAsync())
                .Select(game => new GameDto
                {
                    Id = game.Id,
                    Title = game.Title,
                    Description = game.Description,
                    Rating = game.Rating,
                    ReleaseDate = game.ReleaseDate,
                    DevName = game.Dev.Title,
                    PublisherName = game.Publisher.Title,
                });

            return Result.Success(data);
        }
    }
}
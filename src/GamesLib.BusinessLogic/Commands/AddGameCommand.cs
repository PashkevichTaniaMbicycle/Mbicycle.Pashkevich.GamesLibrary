using MediatR;
using GamesLib.BusinessLogic.Wrappers;
using GamesLib.DataAccess.Model;
using GamesLib.DataAccess.Repositories;

namespace GamesLib.BusinessLogic.Commands
{
    public class AddGameCommand : IRequest<Result<int>>
    {
        public AddGameCommand(
            int devId,
            int publisherId,
            string title,
            string description,
            DateTime releaseDate,
            int rating
            )
        {
            DevId = devId;
            PublisherId = publisherId;
            Title = title;
            Description = description;
            ReleaseDate = releaseDate;
            Rating = rating;
        }
        
        public int DevId { get; set; }
        
        public int PublisherId { get; set; }

        public string Title { get; set; }
        
        public string Description { get; set; }
        
        public DateTime ReleaseDate { get; set; }
        
        public int Rating { get; set; }

    }

    public class AddGameCommandHandler : IRequestHandler<AddGameCommand, Result<int>>
    {
        private readonly IDevRepository _devRepository;
        
        private readonly IPublisherRepository _publisherRepository;

        private readonly IGameRepository _gameRepository;

        public AddGameCommandHandler(
            IDevRepository devRepository,
            IPublisherRepository publisherRepository,
            IGameRepository gameRepository)
        {
            _devRepository = devRepository ?? throw new ArgumentNullException(nameof(devRepository)); ;
            _publisherRepository = publisherRepository ?? throw new ArgumentNullException(nameof(publisherRepository)); ;
            _gameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository)); ;
        }

        public Task<Result<int>> Handle(AddGameCommand command, CancellationToken cancellationToken)
        {
            var dev = _devRepository.Get(command.DevId);
            var publisher = _publisherRepository.Get(command.PublisherId);

            if (dev.Id != command.DevId)
            {
                return Result.FailAsync<int>($"Could not find dev with Id = '{command.DevId}'");
            }
            
            if (publisher.Id != command.PublisherId)
            {
                return Result.FailAsync<int>($"Could not find publisher with Id = '{command.PublisherId}'");
            }

            var newGame = new Game()
            {
                Dev = dev,
                Publisher = publisher,
                Title = command.Title,
                Description = command.Description,
                Rating = command.Rating,
                ReleaseDate = command.ReleaseDate,
            };

            var result = _gameRepository.Add(newGame);

            return Result.SuccessAsync(result.Id);
        }
    }
}

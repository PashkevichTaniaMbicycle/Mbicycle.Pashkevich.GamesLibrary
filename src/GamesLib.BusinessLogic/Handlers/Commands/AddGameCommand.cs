using GamesLib.BusinessLogic.Wrappers.Result;
using GamesLib.DataAccess.Repositories;

namespace GamesLib.BusinessLogic.Handlers.Commands
{
    public class AddGameCommand : IRequestResult<int>
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

    public class AddGameCommandHandler : IRequestHandlerResult<AddGameCommand, int>
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

        public async Task<Result<int>> Handle(AddGameCommand command, CancellationToken cancellationToken)
        {
            var devExists = await _devRepository.ExistById(command.DevId);
            var publisherExists = await _publisherRepository.ExistById(command.PublisherId);

            if (!devExists)
            {
                return Result.Fail<int>($"Could not find dev with Id = '{command.DevId}'");
            }
            
            if (!publisherExists)
            {
                return Result.Fail<int>($"Could not find publisher with Id = '{command.PublisherId}'");
            }

            var data = await _gameRepository.AddAsync(
                command.DevId,
                command.PublisherId,
                command.ReleaseDate,
                command.Rating,
                command.Title,
                command.Description
            );
            
            return Result.Success(data);
        }
    }
}

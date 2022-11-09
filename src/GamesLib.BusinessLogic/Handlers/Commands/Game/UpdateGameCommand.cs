using FluentValidation;
using GamesLib.BusinessLogic.Extensions;
using GamesLib.BusinessLogic.Wrappers.Result;
using GamesLib.DataAccess.Repositories;

namespace GamesLib.BusinessLogic.Handlers.Commands.Game;

public class UpdateGameCommand : IRequestResult<int>
{
    public UpdateGameCommand(
        int gameId,
        int devId,
        int publisherId,
        string title,
        string description,
        DateTime releaseDate,
        int rating
        )
    {
        GameId = gameId;
        DevId = devId;
        PublisherId = publisherId;
        Title = title;
        Description = description;
        ReleaseDate = releaseDate;
        Rating = rating;
    }

    public int GameId { get; set; }
    
    public int DevId { get; set; }
        
    public int PublisherId { get; set; }

    public string Title { get; set; }
        
    public string Description { get; set; }
        
    public DateTime ReleaseDate { get; set; }
        
    public int Rating { get; set; }
}

public class UpdateGameCommandValidator : AbstractValidator<UpdateGameCommand>
{
    private const int MinPositive = 0;
    
    private const int MaxRating = 100;
    
    public UpdateGameCommandValidator()
    {
        RuleFor(x => x.GameId)
            .GreaterThan(MinPositive)
            .WithMessage($"GameId have to be greater then '{MinPositive}'");
        
        RuleFor(x => x.DevId)
            .GreaterThan(MinPositive)
            .WithMessage($"DevId have to be greater then '{MinPositive}'");
        
        RuleFor(x => x.PublisherId)
            .GreaterThan(MinPositive)
            .WithMessage($"PublisherId have to be greater then '{MinPositive}'");
        
        RuleFor(x => x.Rating)
            .GreaterThan(MinPositive)
            .LessThanOrEqualTo(MaxRating)
            .WithMessage($"Rating have to be in range 1-100");
        
        RuleFor(x => x.ReleaseDate)
            .NotNull()
            .WithMessage("Release Date have to not be empty");
    }
}

public class UpdateGameCommandHandler : IRequestHandlerResult<UpdateGameCommand, int>
{
    private readonly IValidator<UpdateGameCommand> _validator;
    private readonly IGameRepository _gameRepository;

    public UpdateGameCommandHandler(
        IValidator<UpdateGameCommand> validator,
        IGameRepository gameRepository)
    {
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _gameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
    }
    
    public async Task<Result<int>> Handle(UpdateGameCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result.Fail<int>(validationResult.GetErrorMessages());
        }
        
        var gameExist = await _gameRepository.ExistById(command.GameId);
        if (!gameExist)
        {
            return Result.Fail<int>($"Could not find game with Id = '{command.GameId}'");
        }

        var data = await _gameRepository.UpdateAsync(
            command.GameId, 
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
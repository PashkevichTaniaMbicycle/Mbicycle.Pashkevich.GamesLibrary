using FluentValidation;
using GamesLib.BusinessLogic.Extensions;
using GamesLib.BusinessLogic.Wrappers.Result;
using GamesLib.DataAccess.Repositories;

namespace GamesLib.BusinessLogic.Handlers.Commands.Game;

public class DeleteGameCommand : IRequestResult<int>
{
    public int GameId { get; set; }
}

public class DeleteGameCommandValidator : AbstractValidator<DeleteGameCommand>
{
    private const int MinIdValue = 0;
    
    public DeleteGameCommandValidator()
    {
        RuleFor(x => x.GameId)
            .GreaterThan(MinIdValue)
            .WithMessage($"GameId have to be greater then '{MinIdValue}'");
    }
}

public class DeleteGameCommandHandler : IRequestHandlerResult<DeleteGameCommand, int>
{
    private readonly IGameRepository _gameRepository;

    private readonly IValidator<DeleteGameCommand> _validator;

    public DeleteGameCommandHandler(
        IValidator<DeleteGameCommand> validator,
        IGameRepository gameRepository)
    {
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _gameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
    }
    
    public async Task<Result<int>> Handle(DeleteGameCommand command, CancellationToken cancellationToken)
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
        
        await _gameRepository.DeleteAsync(command.GameId);
        
        return Result.Success(command.GameId);
    }
}
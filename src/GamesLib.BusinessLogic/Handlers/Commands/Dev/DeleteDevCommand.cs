using FluentValidation;
using GamesLib.BusinessLogic.Extensions;
using GamesLib.BusinessLogic.Wrappers.Result;
using GamesLib.DataAccess.Repositories;

namespace GamesLib.BusinessLogic.Handlers.Commands.Dev;

public class DeleteDevCommand : IRequestResult<int>
{
    public int DevId { get; set; }
}

public class DeleteDevCommandValidator : AbstractValidator<DeleteDevCommand>
{
    private const int MinIdValue = 0;
    
    public DeleteDevCommandValidator()
    {
        RuleFor(x => x.DevId)
            .GreaterThan(MinIdValue)
            .WithMessage($"DevId have to be greater then '{MinIdValue}'");
    }
}

public class DeleteDevCommandHandler : IRequestHandlerResult<DeleteDevCommand, int>
{
    private readonly IDevRepository _devRepository;

    private readonly IValidator<DeleteDevCommand> _validator;

    public DeleteDevCommandHandler(
        IValidator<DeleteDevCommand> validator,
        IDevRepository devRepository)
    {
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _devRepository = devRepository ?? throw new ArgumentNullException(nameof(devRepository));
    }
    
    public async Task<Result<int>> Handle(DeleteDevCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result.Fail<int>(validationResult.GetErrorMessages());
        }
        
        var devExist = await _devRepository.ExistById(command.DevId);
        if (!devExist)
        {
            return Result.Fail<int>($"Could not find dev with Id = '{command.DevId}'");
        }
        
        await _devRepository.DeleteAsync(command.DevId);
        
        return Result.Success(command.DevId);
    }
}
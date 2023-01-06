using FluentValidation;
using GamesLib.BusinessLogic.Extensions;
using GamesLib.BusinessLogic.Wrappers.Result;
using GamesLib.DataAccess.Repositories;

namespace GamesLib.BusinessLogic.Handlers.Commands.Dev;

public class UpdateDevCommand : IRequestResult<int>
{
    public int  DevId { get; set; }
    
    public string DevTitle { get; set; }
    
    public string DevDescription { get; set; }
}

public class UpdateDevCommandValidator : AbstractValidator<UpdateDevCommand>
{
    private const int MinIdValue = 0;
    private const int DevTitleMaxLength = 50;
    
    public UpdateDevCommandValidator()
    {
        RuleFor(x => x.DevId)
            .GreaterThan(MinIdValue)
            .WithMessage($"DevId have to be greater then '{MinIdValue}'");
        
        RuleFor(x => x.DevTitle)
            .MaximumLength(DevTitleMaxLength)
            .WithMessage(x=> $"DevTitle length have to be less then '{DevTitleMaxLength}', actual length is '{x.DevTitle.Length}'");
    }
}

public class UpdateDevCommandHandler : IRequestHandlerResult<UpdateDevCommand, int>
{
    private readonly IValidator<UpdateDevCommand> _validator;
    private readonly IDevRepository _devRepository;

    public UpdateDevCommandHandler(
        IValidator<UpdateDevCommand> validator,
        IDevRepository devRepository)
    {
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _devRepository = devRepository ?? throw new ArgumentNullException(nameof(devRepository));
    }
    
    public async Task<Result<int>> Handle(UpdateDevCommand command, CancellationToken cancellationToken)
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

        var data = await _devRepository.UpdateAsync(
            command.DevId, 
            command.DevTitle,
            command.DevDescription
            );
        
        return Result.Success(data);
    }
}
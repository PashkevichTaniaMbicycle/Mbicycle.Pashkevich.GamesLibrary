using FluentValidation;
using GamesLib.BusinessLogic.Extensions;
using GamesLib.BusinessLogic.Wrappers.Result;
using GamesLib.DataAccess.Repositories;

namespace GamesLib.BusinessLogic.Handlers.Commands.Dev;

public class AddDevCommand : IRequestResult<int>
{
    public string DevTitle { get; set; }
}

public class AddDevCommandValidator : AbstractValidator<AddDevCommand>
{
    private const int DevTitleMaxLength = 50;
    
    public AddDevCommandValidator()
    {
        RuleFor(x => x.DevTitle)
            .MaximumLength(DevTitleMaxLength)
            .WithMessage(x=> $"DevTitle length have to be less then '{DevTitleMaxLength}', actual length is '{x.DevTitle.Length}'");
    }
}

public class AddDevCommandHandler : IRequestHandlerResult<AddDevCommand, int>
{
    private readonly IValidator<AddDevCommand> _validator;
    private readonly IDevRepository _devRepository;

    public AddDevCommandHandler(
        IValidator<AddDevCommand> validator,
        IDevRepository devRepository)
    {
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _devRepository = devRepository ?? throw new ArgumentNullException(nameof(devRepository));
    }
    
    public async Task<Result<int>> Handle(AddDevCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result.Fail<int>(validationResult.GetErrorMessages());
        }
        
        var exist = await _devRepository.ExistByTitle(command.DevTitle);
        if (exist)
        {
            return Result.Fail<int>($@"Could not add dev with name '{command.DevTitle}' which is already exists");
        }

        var data = await _devRepository.AddAsync(command.DevTitle);
        
        return Result.Success(data);
    }
}
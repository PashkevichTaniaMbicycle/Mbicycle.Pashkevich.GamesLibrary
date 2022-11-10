using FluentValidation;
using GamesLib.BusinessLogic.Extensions;
using GamesLib.BusinessLogic.Wrappers.Result;
using GamesLib.DataAccess.Repositories;

namespace GamesLib.BusinessLogic.Handlers.Commands.Publisher;

public class AddPublisherCommand : IRequestResult<int>
{
    public string Title { get; set; }
    
    public string Description { get; set; }
}

public class AddPublisherCommandValidator : AbstractValidator<AddPublisherCommand>
{
    private const int TitleMaxLength = 50;
    
    public AddPublisherCommandValidator()
    {
        RuleFor(x => x.Title)
            .MaximumLength(TitleMaxLength)
            .WithMessage(x=> $"Title length have to be less then '{TitleMaxLength}', actual length is '{x.Title.Length}'");
    }
}

public class AddPublisherCommandHandler : IRequestHandlerResult<AddPublisherCommand, int>
{
    private readonly IValidator<AddPublisherCommand> _validator;
    private readonly IPublisherRepository _publisherRepository;

    public AddPublisherCommandHandler(
        IValidator<AddPublisherCommand> validator,
        IPublisherRepository publisherRepository)
    {
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _publisherRepository = publisherRepository ?? throw new ArgumentNullException(nameof(publisherRepository));
    }
    
    public async Task<Result<int>> Handle(AddPublisherCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result.Fail<int>(validationResult.GetErrorMessages());
        }
        
        var exist = await _publisherRepository.ExistByTitle(command.Title);
        if (exist)
        {
            return Result.Fail<int>($@"Could not add publisher with name '{command.Title}' which is already exists");
        }

        var data = await _publisherRepository.AddAsync(command.Title, command.Description);
        
        return Result.Success(data);
    }
}
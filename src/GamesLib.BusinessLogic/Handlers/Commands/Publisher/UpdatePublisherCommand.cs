using FluentValidation;
using GamesLib.BusinessLogic.Extensions;
using GamesLib.BusinessLogic.Wrappers.Result;
using GamesLib.DataAccess.Repositories;

namespace GamesLib.BusinessLogic.Handlers.Commands.Publisher;

public class UpdatePublisherCommand : IRequestResult<int>
{
    public int  PublisherId { get; set; }
    
    public string Title { get; set; }
    
    public string Description { get; set; }
}

public class UpdatePublisherCommandValidator : AbstractValidator<UpdatePublisherCommand>
{
    private const int MinIdValue = 0;
    private const int TitleMaxLength = 50;
    
    public UpdatePublisherCommandValidator()
    {
        RuleFor(x => x.PublisherId)
            .GreaterThan(MinIdValue)
            .WithMessage($"PublisherId have to be greater then '{MinIdValue}'");
        
        RuleFor(x => x.Title)
            .MaximumLength(TitleMaxLength)
            .WithMessage(x=> $"Title length have to be less then '{TitleMaxLength}', actual length is '{x.Title.Length}'");
    }
}

public class UpdatePublisherCommandHandler : IRequestHandlerResult<UpdatePublisherCommand, int>
{
    private readonly IValidator<UpdatePublisherCommand> _validator;
    private readonly IPublisherRepository _publisherRepository;

    public UpdatePublisherCommandHandler(
        IValidator<UpdatePublisherCommand> validator,
        IPublisherRepository publisherRepository)
    {
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _publisherRepository = publisherRepository ?? throw new ArgumentNullException(nameof(publisherRepository));
    }
    
    public async Task<Result<int>> Handle(UpdatePublisherCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result.Fail<int>(validationResult.GetErrorMessages());
        }
        
        var publisherExist = await _publisherRepository.ExistById(command.PublisherId);
        if (!publisherExist)
        {
            return Result.Fail<int>($"Could not find publisher with Id = '{command.PublisherId}'");
        }

        var data = await _publisherRepository.UpdateAsync(
            command.PublisherId, 
            command.Title,
            command.Description);
        
        return Result.Success(data);
    }
}
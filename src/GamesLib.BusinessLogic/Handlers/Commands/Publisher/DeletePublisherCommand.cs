using FluentValidation;
using GamesLib.BusinessLogic.Extensions;
using GamesLib.BusinessLogic.Wrappers.Result;
using GamesLib.DataAccess.Repositories;

namespace GamesLib.BusinessLogic.Handlers.Commands.Publisher;

public class DeletePublisherCommand : IRequestResult<int>
{
    public int PublisherId { get; set; }
}

public class DeletePublisherCommandValidator : AbstractValidator<DeletePublisherCommand>
{
    private const int MinIdValue = 0;
    
    public DeletePublisherCommandValidator()
    {
        RuleFor(x => x.PublisherId)
            .GreaterThan(MinIdValue)
            .WithMessage($"PublisherId have to be greater then '{MinIdValue}'");
    }
}

public class DeletePublisherCommandHandler : IRequestHandlerResult<DeletePublisherCommand, int>
{
    private readonly IPublisherRepository _publisherRepository;

    private readonly IValidator<DeletePublisherCommand> _validator;

    public DeletePublisherCommandHandler(
        IValidator<DeletePublisherCommand> validator,
        IPublisherRepository publisherRepository)
    {
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _publisherRepository = publisherRepository ?? throw new ArgumentNullException(nameof(publisherRepository));
    }
    
    public async Task<Result<int>> Handle(DeletePublisherCommand command, CancellationToken cancellationToken)
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
        
        await _publisherRepository.DeleteAsync(command.PublisherId);
        
        return Result.Success(command.PublisherId);
    }
}
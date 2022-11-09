using MediatR;

namespace GamesLib.BusinessLogic.Wrappers.Result;

public interface IRequestResult<TOut> : IRequest<Result<TOut>> { }
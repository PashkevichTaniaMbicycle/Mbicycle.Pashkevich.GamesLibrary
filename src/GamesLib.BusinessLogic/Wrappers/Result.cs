namespace GamesLib.BusinessLogic.Wrappers

{
    public static class Result
    {
        public static Result<T> Success<T>(T data, string message = default) => new(data, false, message);

        public static Result<T> Fail<T>(string message, T data = default) => new(data, true, message);

        public static Task<Result<T>> SuccessAsync<T>(T data, string message = default) => Task.FromResult(new Result<T>(data, false, message));

        public static Task<Result<T>> FailAsync<T>(string message, T data = default) => Task.FromResult(new Result<T>(data, true, message));
    }

    public class Result<T>
    {
        public Result(T data, bool error, string message)
        {
            Data = data;
            Error = error; 
            Message = message;
        }

        public T Data { get; }

        public bool Error { get; }

        public string Message { get; }
    }
}
namespace GamesLib.BusinessLogic.Wrappers.Result
{
    public static class Result
    {
        public static Result<T> Success<T>(T data, string message = default) => new(data, false, message);

        public static Result<T> Fail<T>(string message, T data = default) => new(data, true, message);
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
namespace GamesLib.BusinessLogic.Wrappers.Result;
public static class Result
{
    public static Result<T> Success<T>(T data) => new(data, true);

    public static Result<T> Success<T>(T data, string message) => new(data, true, message);

    public static Result<T> Success<T>(T data, IEnumerable<string> messages) => new(data, true, messages);

    public static Result<T> Fail<T>(string message) => new(false, message);

    public static Result<T> Fail<T>(IEnumerable<string> messages) => new(false, messages);

    public static Result<T> Fail<T>(T data, string message) => new(data, false, message);

    public static Result<T> Fail<T>(T data, IEnumerable<string> messages) => new(data, false, messages);
}

public class Result<T>
{
    public Result(T data, bool success)
    {
        Data = data;
        Success = success;
        Messages = new List<string>();
    }

    public Result(bool success, string message)
    {
        Data = default!;
        Success = success;
        Messages = new List<string> { message };
    }

    public Result(bool success, IEnumerable<string> messages)
    {
        Data = default!;
        Success = success;
        Messages = messages.ToList();
    }

    public Result(T data, bool success, string message)
    {
        Data = data;
        Success = success;
        Messages = new List<string> { message };
    }

    public Result(T data, bool success, IEnumerable<string> messages)
    {
        Data = data;
        Success = success;
        Messages = messages.ToList();
    }

    public T Data { get; }

    public bool Success { get; }

    public List<string> Messages { get; }
}
namespace PetHealth.Application.Common.Results;

public class Result
{
    
    public Error? Error { get; }
    public bool IsSuccess => Error == null;

    private Result()
    {
        
    }

    private Result(Error error)
    {
        Error = error;
    }

    public static Result Success() => new();
    public static Result Failure(Error error) => new(error);
}

public class Result<T>
{
    public T? Data { get; set; }
    public Error? Error { get; set; }

    public bool IsSuccess => Error == null;

    private Result(T? data) => Data = data;
    private Result(Error? error) => Error = error;

    public static Result<T> Success(T value) => new(value);
    public static Result<T> Failure(Error error) => new(error);
}
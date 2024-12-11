namespace SharedKernel.Models;

public class Result<TResult>
{
    protected Result(string message, TResult successResult, ResultStatus status)
    {
        Message = message;
        SuccessResult = successResult;
        Status = status;
    }

    // Had to make a public Constructor to be able to Deserialize a JSON String into the Entity
    public Result()
    {
    }

    public string Message { get; set; }
    public TResult SuccessResult { get; set; }
    public ResultStatus Status { get; set; }

    public static Result<TResult> Create(string message, TResult successResult, ResultStatus status) 
        => new(message, successResult, status);
}

public enum ResultStatus
{
    Success,
    Warning,
    Error,
    Created,
    Updated,
    Added,
    Deleted,
    UnAuthorized
}
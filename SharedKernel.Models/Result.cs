namespace SharedKernel.Models;

public class Result<TResult>
{
    public string Message { get; set; }
    public TResult SuccessResult { get; set; }
    public ResultStatus Status { get; set; }

    protected Result(string message, TResult successResult, ResultStatus status)
    {
        Message = message;
        SuccessResult = successResult;
        Status = status;
    }
    
    public static Result<TResult> Create(string message, TResult successResult, ResultStatus status)
    {
        return new Result<TResult>(message, successResult, status);
    }
}

public enum ResultStatus
{
    Success,
    Warning,
    Error,
    Created,
    Updated,
    Added,
    Deleted
}
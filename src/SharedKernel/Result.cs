namespace ComponentName.SharedKernel;

// Simple implementation Result Pattern
// https://medium.com/@wgyxxbf/result-pattern-a01729f42f8c
public class Result<TValue, TError>
{
    protected Result() { }

    public readonly TValue? Value;
    public readonly TError? Error;
    protected bool _isSuccess { private get; init; }

    private Result(TValue value)
    {
        _isSuccess = true;
        Value = value;
        Error = default;
    }

    private Result(TError error)
    {
        _isSuccess = false;
        Value = default;
        Error = error;
    }

    public static implicit operator Result<TValue, TError>(TValue value) => new(value);

    public static implicit operator Result<TValue, TError>(TError error) => new(error);

    public TMatchResult Match<TMatchResult>(
        Func<TValue, TMatchResult> success,
        Func<TError, TMatchResult> failure
    )
    {
        if (_isSuccess)
        {
            return success(Value!);
        }
        return failure(Error!);
    }
}

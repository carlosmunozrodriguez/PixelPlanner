namespace PixelPlanner.Entities;

public abstract record Result<T>
{
    public bool Success { get; protected init; }

    public static SuccessfulResult<T> Successful(T value) => new(value);

    public static FailedResult<T> Failed(string error) => new(error);
}

public record SuccessfulResult<T> : Result<T>
{
    public T Value { get; }

    public SuccessfulResult(T value)
    {
        Success = true;
        Value = value;
    }
}

public record FailedResult<T> : Result<T>
{
    public string Error { get; }

    public FailedResult(string error)
    {
        Success = false;
        Error = error;
    }
}

/// <summary>
/// Represents a functional type for not returning anything
/// </summary>
public record Void
{
    public static Void Nothing { get; } = new();

    private Void() { }
}
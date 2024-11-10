namespace Module.Semester.Domain.ValueObjects;

public record Capacity
{
    public int Value { get; init; }

    public Capacity(int value)
    {
        Validate(value);
        Value = value;
    }

    private void Validate(int value)
    {
        if (value <= 0)
            throw new ArgumentException("Capacity must be greater than zero.");
    }

    public static implicit operator Capacity(int value) 
        => new(value);
}
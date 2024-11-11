namespace Module.Semester.Domain.ValueObjects;

public record StudentCapacity
{
    public int Value { get; init; }

    public StudentCapacity(int value)
    {
        Validate(value);
        Value = value;
    }

    private void Validate(int value)
    {
        if (value <= 0)
            throw new ArgumentException("Capacity must be greater than zero.");
    }

    public static implicit operator StudentCapacity(int value) 
        => new(value);
}
namespace School.Domain.ValueObjects;

public record StudentCapacity
{
    public StudentCapacity(int value)
    {
        Validate(value);
        Value = value;
    }

    public int Value { get; init; }

    private void Validate(int value)
    {
        if (value <= 0)
            throw new ArgumentException("Capacity must be greater than zero.");
    }

    public static implicit operator StudentCapacity(int value)
    {
        return new StudentCapacity(value);
    }

    public static implicit operator int(StudentCapacity value)
    {
        return value.Value;
    }
}
namespace School.Domain.ValueObjects;

public record SemesterNumber
{
    public SemesterNumber(int value)
    {
        Validate(value);
        Value = value;
    }

    public int Value { get; init; }

    private void Validate(int value)
    {
        switch (value)
        {
            case <= 0:
                throw new ArgumentException("Semester number cannot be less or equal to zero.");
            case > 12:
                throw new ArgumentException("Semester number cannot be more than 12.");
        }
    }

    public static implicit operator int(SemesterNumber number)
    {
        return number.Value;
    }

    public static implicit operator SemesterNumber(int number)
    {
        return new SemesterNumber(number);
    }
}
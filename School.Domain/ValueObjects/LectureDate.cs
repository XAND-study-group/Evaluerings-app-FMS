namespace School.Domain.ValueObjects;

public class LectureDate
{
    public LectureDate(DateOnly value)
    {
        Validate(value);
        Value = value;
    }

    public DateOnly Value { get; init; }

    private void Validate(DateOnly value)
    {
        if (value < DateOnly.FromDateTime(DateTime.Now))
            throw new ArgumentException("Lecture date cannot be earlier than today.");
    }

    public static implicit operator DateOnly(LectureDate value) => value.Value;

    public static implicit operator LectureDate(DateOnly value) => new(value);
}
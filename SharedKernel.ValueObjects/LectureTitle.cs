namespace Module.Semester.Domain.ValueObjects;

public record LectureTitle
{
    public string Value { get; init; }

    public LectureTitle(string value)
    {
        Validate(value);
        Value = value;
    }

    private void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Lecture title cannot be null or whitespace.", nameof(value));
        
        if (value.Length > 100)
            throw new ArgumentException("Lecture title cannot be longer than 100 characters.", nameof(value));
    }
    
    public static implicit operator string(LectureTitle lectureTitle) => lectureTitle.Value;
    public static implicit operator LectureTitle(string value) => new(value);
}
namespace SharedKernel.ValueObjects;

public record SemesterName
{
    public string Value { get; init; }

    private SemesterName(string value, IEnumerable<string> otherSemesterNames)
    {
        Validate(value, otherSemesterNames);
        Value = value;
    }

    public static SemesterName Create(string name, IEnumerable<string> otherSemesterNames)
        => new SemesterName(name, otherSemesterNames);

    private void Validate(string value, IEnumerable<string> otherSemesterNames)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Semester name kan ikke være null eller whitespace.", nameof(value));

        if (value.Length > 50)
            throw new ArgumentException("Semester name kan ikke være længere end 50 karakterer.", nameof(value));

        if (otherSemesterNames.Any(otherName => otherName == value))
            throw new ArgumentException($"A Semester with name '{value}' already exists.");
    }

    public static implicit operator string(SemesterName value) => value.Value;
}
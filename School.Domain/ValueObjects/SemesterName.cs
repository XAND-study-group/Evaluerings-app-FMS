namespace School.Domain.ValueObjects;

public record SemesterName
{
    private SemesterName(string value)
    {
        Validate(value);
        Value = value;
    }

    public string Value { get; init; }

    public static SemesterName Create(string name, IEnumerable<string> otherSemesterNames)
    {
        var semesterName = new SemesterName(name);
        AssureIsUniqueName(otherSemesterNames, name);
        return semesterName;
    }

    private static void AssureIsUniqueName(IEnumerable<string> otherSemesterNames, string name)
    {
        if (otherSemesterNames.Any(otherName => otherName == name))
            throw new ArgumentException($"A Semester with name '{name}' already exists.");
    }

    private void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Semester name kan ikke være null eller whitespace.", nameof(value));

        if (value.Length > 50)
            throw new ArgumentException("Semester name kan ikke være længere end 50 karakterer.", nameof(value));
    }

    public static implicit operator string(SemesterName value) => value.Value;
}
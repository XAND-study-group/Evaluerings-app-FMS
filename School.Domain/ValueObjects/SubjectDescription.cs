namespace School.Domain.ValueObjects;

public sealed class SubjectDescription : IEquatable<SubjectDescription>
{
    public SubjectDescription(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Description cannot be empty or whitespace.", nameof(value));

        if (value.Length > 500)
            throw new ArgumentException("Description cannot be longer than 500 characters.", nameof(value));

        Value = value;
    }

    public string Value { get; init; }

    public bool Equals(SubjectDescription? other) => other != null && Value == other.Value;

    public override bool Equals(object? obj) => obj is SubjectDescription description && Value == description.Value;

    public override int GetHashCode() => Value.GetHashCode();

    public override string ToString() => Value;

    public static implicit operator string(SubjectDescription description) => description.Value;

    public static implicit operator SubjectDescription(string description) => new(description);
}